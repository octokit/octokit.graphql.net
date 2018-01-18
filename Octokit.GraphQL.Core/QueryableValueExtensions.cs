using System;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core;

namespace Octokit.GraphQL
{
    public static class QueryableValueExtensions
    {
        public static IQueryableValue<TResult> Select<TValue, TResult>(
            this IQueryableValue<TValue> source,
            Expression<Func<TValue, TResult>> selector)
                where TValue : IQueryableValue
        {
            return new QueryableValue<TResult>(
                source.Provider,
                Expression.Call(
                    null,
                    GetMethodInfoOf(() => Select(
                        default(IQueryableValue<TValue>),
                        default(Expression<Func<TValue, TResult>>))),
                    new Expression[] { source.Expression, Expression.Quote(selector) }));
        }

        public static TValue Single<TValue>(this IQueryableValue<TValue> source)
        {
            throw new NotImplementedException();
        }

        public static TValue SingleOrDefault<TValue>(this IQueryableValue<TValue> source)
        {
            throw new NotImplementedException();
        }

        public static TResult RewrittenSelect<TResult>(
            JToken source,
            Func<JToken, TResult> selector)
        {
            return selector(source);
        }

        private static MethodInfo GetMethodInfoOf<T>(Expression<Func<T>> expression)
        {
            var body = (MethodCallExpression)expression.Body;
            return body.Method;
        }
    }
}
