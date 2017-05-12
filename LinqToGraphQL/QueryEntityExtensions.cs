using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Octokit.GraphQL.Core
{
    public static class QueryEntityExtensions
    {
        public static IQueryable<TResult> Select<TEntity, TResult>(this TEntity source, Expression<Func<TEntity, TResult>> selector)
            where TEntity : IQueryEntity
        {
            return new FieldQuery<TResult>(
                source.Provider,
                Expression.Call(
                    null,
                    GetMethodInfoOf(() => QueryEntityExtensions.Select(
                        default(TEntity),
                        default(Expression<Func<TEntity, TResult>>))),
                    new Expression[] { source.Expression, Expression.Quote(selector) }));
        }

        private static MethodInfo GetMethodInfoOf<T>(Expression<Func<T>> expression)
        {
            var body = (MethodCallExpression)expression.Body;
            return body.Method;
        }
    }
}
