using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Octokit.GraphQL.Core.Syntax;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL.Core.Utilities
{
    /// <summary>
    /// Extension methods for Expression objects.
    /// </summary>
    static class ExpressionExtensions
    {
        /// <summary>
        /// Remove all the outer quotes from an expression.
        /// </summary>
        /// <param name="expression">Expression that might contain outer quotes.</param>
        /// <returns>Expression that no longer contains outer quotes.</returns>
        public static Expression StripQuotes(this Expression expression)
        {
            while (expression.NodeType == ExpressionType.Quote)
                expression = ((UnaryExpression)expression).Operand;
            return expression;
        }

        /// <summary>
        /// Get the lambda for an expression stripping any necessary outer quotes.
        /// </summary>
        /// <param name="expression">Expression that should be a lamba possibly wrapped
        /// in outer quotes.</param>
        /// <returns>LambdaExpression no longer wrapped in quotes.</returns>
        public static LambdaExpression GetLambda(this Expression expression)
        {
            return (LambdaExpression)expression.StripQuotes();
        }

        public static Expression AddCast(this Expression expression, Type type)
        {
            var sourceType = expression.Type.GetTypeInfo();
            var targetType = type.GetTypeInfo();

            if (targetType.IsAssignableFrom(sourceType))
            {
                // If the sourceType is assignable to the target type, return the source expression.
                return expression;
            }
            else if (typeof(JToken).GetTypeInfo().IsAssignableFrom(sourceType))
            {
                // If the source type is a JToken use JToken.ToObject to convert to the target type.
                return Expression.Call(
                    expression,
                    ExpressionMethods.JTokenToObject.MakeGenericMethod(type));
            }
            else if (IsIQueryableOfJToken(sourceType))
            {
                // If the source type is an IQueryable<JToken> then add a select statement to cast.
                return AddSelectCast(expression, type);
            }

            throw new NotSupportedException(
                $"Don't know how to cast '{expression}' ({expression.Type}) to '{type}'.");
        }

        /// <summary>
        /// Adds an indexer to an expression representing a <see cref="JToken"/>.
        /// </summary>
        /// <param name="instance">The expression.</param>
        /// <param name="field">The field to return.</param>
        /// <returns>A new expression.</returns>
        public static Expression AddIndexer(this Expression instance, FieldSelection field)
        {
            return AddIndexer(instance, field.Alias ?? field.Name);
        }

        /// <summary>
        /// Adds an indexer to an expression representing a <see cref="JToken"/> or a collection of
        /// <see cref="JToken"/>s.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="field">The field to return.</param>
        /// <returns>A new expression.</returns>
        public static Expression AddIndexer(this Expression expression, string fieldName)
        {
            if (expression.Type.GetTypeInfo().IsAssignableFrom(typeof(JToken).GetTypeInfo()))
            {
                // Returns `expression[fieldName];`
                return Expression.Call(
                    expression,
                    ExpressionMethods.JTokenIndexer,
                    Expression.Constant(fieldName));
            }
            else if (IsIQueryableOfJToken(expression.Type))
            {
                // Returns `expression.Select(x => x[fieldName]);`
                var lambdaParameter = Expression.Parameter(typeof(JToken));
                return Expression.Call(
                    ExpressionMethods.SelectMethod.MakeGenericMethod(typeof(JToken)),
                    expression,
                    Expression.Lambda(
                        lambdaParameter.AddIndexer(fieldName),
                        lambdaParameter));
            }
            else
            {
                throw new NotSupportedException($"Don't know how to add an indexer to {expression}.");
            }
        }

        private static Expression AddSelectCast(Expression expression, Type type)
        {
            var targetItemTypeType = GetEnumerableItemType(type);

            if (targetItemTypeType != null)
            {
                // The target type is an IEnumerable<>. 
                var genericTypeArguments = type.GetTypeInfo().GenericTypeArguments;
                var queryType = genericTypeArguments[0];

                if (expression is MethodCallExpression methodCall)
                {
                    if (IsSelect(methodCall.Method))
                    {
                        // The source expression is an ExpressionMethods.Select call. Create a new
                        // ExpressionMethods.Select call with a modified selector lambda which adds
                        // the required cast.
                        // TODO: Is this needed? There are no covering tests.
                        var instance = methodCall.Arguments[0];
                        var lambda = methodCall.Arguments[1].GetLambda();
                        return Expression.Call(
                            ExpressionMethods.SelectMethod.MakeGenericMethod(queryType),
                            instance,
                            Expression.Lambda(
                                lambda.Body.AddCast(queryType),
                                lambda.Parameters));
                    }
                    else if (IsSelectEntity(methodCall.Method))
                    {
                        // The source expression is an ExpressionMethods.SelectEntity call. Create a new
                        // ExpressionMethods.SelectEntity call with a modified selector lambda which adds
                        // the required cast. For example the following expression:
                        //
                        //     ExpressionMethods.SelectEntity(list, x => x["a"])
                        //
                        // Would be rewritten as
                        //
                        //      ExpressionMethods.SelectEntity(list, x => x["a"].ToObject<TargetType>())
                        //
                        var instance = methodCall.Arguments[0];
                        var lambda = methodCall.Arguments[1].GetLambda();
                        return Expression.Call(
                            ExpressionMethods.SelectEntityMethod.MakeGenericMethod(queryType),
                            instance,
                            Expression.Lambda(
                                lambda.Body.AddCast(queryType),
                                lambda.Parameters));
                    }
                }
                else
                {
                    // The source expression is not a Select call; add a new select call on the
                    // IEnumerable<> to cast each element.
                    var parameter = Expression.Parameter(typeof(JToken));
                    return Expression.Call(
                        ExpressionMethods.SelectMethod.MakeGenericMethod(queryType),
                        expression,
                        Expression.Lambda(
                            parameter.AddCast(queryType),
                            parameter));
                }
            }
            else
            {
                // The target type is not an IEnumerable<>. Call ExpressionMethods.FirstOrDefault
                // on the source with a cast to the correct type.
                var parameter = Expression.Parameter(typeof(JToken));
                var result =  Expression.Call(
                    ExpressionMethods.FirstOrDefaultMethod.MakeGenericMethod(type),
                    expression,
                    Expression.Lambda(
                        parameter.AddCast(type),
                        parameter));
                return result;
            }

            throw new NotSupportedException(
                $"Don't know how to cast '{expression}' ({expression.Type}) to '{type}'.");
        }

        private static Type GetEnumerableItemType(Type type)
        {
            if (type == typeof(string))
                return null;

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return type.GetTypeInfo().GenericTypeArguments[0];
            }

            foreach (var i in type.GetTypeInfo().ImplementedInterfaces)
            {
                if (i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return i.GetTypeInfo().GenericTypeArguments[0];
                }
            }

            return null;
        }

        private static bool IsIQueryableOfJToken(Type type)
        {
            return IsIQueryableOfJToken(type.GetTypeInfo());
        }

        private static bool IsIQueryableOfJToken(TypeInfo type)
        {
            return typeof(IQueryable<JToken>).GetTypeInfo().IsAssignableFrom(type);
        }

        private static bool IsSelect(MethodInfo method)
        {
            return (method.DeclaringType == typeof(ExpressionMethods) &&
                method.Name == nameof(ExpressionMethods.Select) &&
                method.GetParameters().Length == 2);
        }

        private static bool IsSelectEntity(MethodInfo method)
        {
            return (method.DeclaringType == typeof(ExpressionMethods) &&
                method.Name == nameof(ExpressionMethods.SelectEntity) &&
                method.GetParameters().Length == 2);
        }
    }
}
