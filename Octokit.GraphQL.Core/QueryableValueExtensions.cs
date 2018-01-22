using System;
using System.Linq.Expressions;
using System.Reflection;
using Octokit.GraphQL.Core;

namespace Octokit.GraphQL
{
    public static class QueryableValueExtensions
    {
        public static readonly MethodInfo SelectMethod = typeof(QueryableValueExtensions).GetTypeInfo().GetDeclaredMethod(nameof(Select));
        public static readonly MethodInfo SingleMethod = typeof(QueryableValueExtensions).GetTypeInfo().GetDeclaredMethod(nameof(Single));
        public static readonly MethodInfo SingleOrDefaultMethod = typeof(QueryableValueExtensions).GetTypeInfo().GetDeclaredMethod(nameof(SingleOrDefault));

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

        private static MethodInfo GetMethodInfoOf<T>(Expression<Func<T>> expression)
        {
            var body = (MethodCallExpression)expression.Body;
            return body.Method;
        }
    }
}
