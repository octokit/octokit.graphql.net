using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Octokit.GraphQL.Core;

namespace Octokit.GraphQL
{
    public static class QueryableListExtensions
    {
        public static IQueryableList<TResult> OfType<TResult>(this IQueryableList source)
            where TResult : IQueryableValue
        {
            return new QueryableList<TResult>(
                source.Provider,
                Expression.Call(
                    null,
                    GetMethodInfoOf(() => OfType<TResult>(default(IQueryableList))),
                    new Expression[] { source.Expression }));
        }

        public static IQueryableList<TResult> Select<TValue, TResult>(
            this IQueryableList<TValue> source,
            Expression<Func<TValue, TResult>> selector)
                where TValue : IQueryableValue
        {
            return new QueryableList<TResult>(
                source.Provider,
                Expression.Call(
                    null,
                    GetMethodInfoOf(() => Select(
                        default(IQueryableList<TValue>),
                        default(Expression<Func<TValue, TResult>>))),
                    new Expression[] { source.Expression, Expression.Quote(selector) }));
        }

        public static IDictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
            this IQueryableList<TSource> source,
            Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector)
        {
            throw new NotImplementedException();
        }

        public static IList<TValue> ToList<TValue>(this IQueryableList<TValue> source)
        {
            throw new NotImplementedException();
        }

        private static MethodInfo GetMethodInfoOf<T>(Expression<Func<T>> expression)
        {
            var body = (MethodCallExpression)expression.Body;
            return body.Method;
        }
    }
}
