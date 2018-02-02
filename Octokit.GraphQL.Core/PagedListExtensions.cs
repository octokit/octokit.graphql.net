using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Internal;

namespace Octokit.GraphQL
{
    public static class PagedListExtensions
    {
        public static readonly MethodInfo SelectMethod = GetMethodInfo(nameof(SelectMethod));
        public static readonly MethodInfo ToListMethod = GetMethodInfo(nameof(ToListMethod));

        [MethodId(nameof(SelectMethod))]
        public static IPagedList<IPagingConnection<TResult>> Select<TValue, TResult>(
            this IPagedList<IPagingConnection<TValue>> source,
            Expression<Func<TValue, TResult>> selector)
                where TValue : IQueryableValue
        {
            return new PagedList<IPagingConnection<TResult>>(
                Expression.Call(
                    null,
                    GetMethodInfoOf(() => Select(
                        default(PagedList<IPagingConnection<TValue>>),
                        default(Expression<Func<TValue, TResult>>))),
                    new Expression[] { source.Expression, Expression.Quote(selector) }));
        }

        [MethodId(nameof(ToListMethod))]
        public static IList<TValue> ToList<TValue>(this IPagedList<IPagingConnection<TValue>> source)
        {
            throw new NotImplementedException();
        }

        public static IQuery<IEnumerable<T>> Compile<T>(this IPagedList<IPagingConnection<T>> expression)
        {
            return new PagedQueryBuilder().Build(expression);
        }

        private static MethodInfo GetMethodInfo(string id)
        {
            return typeof(PagedListExtensions)
                .GetTypeInfo()
                .DeclaredMethods
                .Where(x => x.GetCustomAttribute<MethodIdAttribute>()?.Id == id)
                .SingleOrDefault();
        }

        private static MethodInfo GetMethodInfoOf<T>(Expression<Func<T>> expression)
        {
            var body = (MethodCallExpression)expression.Body;
            return body.Method;
        }
    }
}
