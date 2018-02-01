using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL
{
    public static class PagedListExtensions
    {
        public static IPagedList<IQueryableList<TResult>> Select<TValue, TResult>(
            this IPagedList<IQueryableList<TValue>> source,
            Expression<Func<TValue, TResult>> selector)
                where TValue : IQueryableValue
        {
            return new PagedList<IQueryableList<TResult>>(source.Expression, source.Method, selector);
        }

        public static IQuery<IEnumerable<T>> Compile<T>(this IPagedList<T> expression)
            where T : IQueryableList
        {
            return new PagedQueryBuilder().Build(expression);
        }
    }
}
