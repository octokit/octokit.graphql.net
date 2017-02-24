using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace LinqToGraphQL.Utilities
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
            if (expression.Type == type)
            {
                return expression;
            }
            else if (typeof(JToken).IsAssignableFrom(expression.Type))
            {
                return Expression.Call(
                    expression,
                    ExpressionMethods.JTokenToObject.MakeGenericMethod(type));
            }
            else if (GetQueryableResultType(type) != null &&
                     GetQueryableResultType(expression.Type) == typeof(JToken))
            {
                var queryType = type.GetGenericArguments()[0];
                var methodCall = expression as MethodCallExpression;

                if (methodCall != null && IsJsonSelect(methodCall.Method))
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

            throw new NotImplementedException(
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

        private static Type GetQueryableResultType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IQueryable<>) ?
                type.GetGenericArguments()[0] : null;
        }

        private static bool IsJsonSelect(MethodInfo method)
        {
            return (method.DeclaringType == typeof(ExpressionMethods) &&
                method.Name == nameof(ExpressionMethods.SelectEntity) &&
                method.GetParameters().Length == 2);
        }
    }
}
