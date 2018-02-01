using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL
{
    public static class PagedListExtensions
    {
        public static IPagedList<IPagingConnection<TResult>> Select<TValue, TResult>(
            this IPagedList<IPagingConnection<TValue>> source,
            Expression<Func<TValue, TResult>> selector)
                where TValue : IQueryableValue
        {
            return new PagedList<IPagingConnection<TResult>>(source.Expression, selector);
        }

        public static IQuery<IEnumerable<T>> Compile<T>(this IPagedList<T> expression)
            where T : IPagingConnection
        {
            return new PagedQueryBuilder().Build(expression);
        }
    }
}
