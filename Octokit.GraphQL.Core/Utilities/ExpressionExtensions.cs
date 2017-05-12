using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
            if (type.IsAssignableFrom(expression.Type))
            {
                return expression;
            }
            else if (typeof(JToken).IsAssignableFrom(expression.Type))
            {
                return Expression.Call(
                    expression,
                    ExpressionMethods.JTokenToObject.MakeGenericMethod(type));
            }
            else if (GetEnumerableResultType(type) != null && IsIQueryableOfJToken(expression.Type))
            {
                var queryType = type.GetGenericArguments()[0];

                if (expression is MethodCallExpression methodCall)
                {
                    if (IsSelect(methodCall.Method))
                    {
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
                    var parameter = Expression.Parameter(typeof(JToken));
                    return Expression.Call(
                        ExpressionMethods.SelectMethod.MakeGenericMethod(queryType),
                        expression,
                        Expression.Lambda(
                            parameter.AddCast(queryType),
                            parameter));
                }
            }

            throw new NotSupportedException(
                $"Don't know how to cast '{expression}' to '{type}'.");
        }

        /// <summary>
        /// Adds an indexer to an expression representing a <see cref="JToken"/>.
        /// </summary>
        /// <param name="instance">The expression.</param>
        /// <param name="parameter">The indexer parameter</param>
        /// <returns>A new expression.</returns>
        public static Expression AddIndexer(this Expression instance, string parameter)
        {
            return Expression.Call(
                instance,
                ExpressionMethods.JTokenIndexer,
                Expression.Constant(parameter));
        }

        private static Type GetEnumerableResultType(Type type)
        {
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
            {
                return type.GetGenericArguments()[0];
            }

            foreach (var i in type.GetInterfaces())
            {
                if (i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    return i.GetGenericArguments()[0];
                }
            }

            return null;
        }

        private static bool IsIQueryableOfJToken(Type type)
        {
            return typeof(IQueryable<JToken>).IsAssignableFrom(type);
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
