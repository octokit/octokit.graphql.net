using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public class PagedList<T> : IPagedList<T>
        where T : IPagingConnection
    {
        public PagedList(Expression expression)
        {
            Expression = expression;
        }

        public Expression Expression { get; }
    }
}
