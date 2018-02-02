using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public class PagedList<T> : IPagedList<T>
        where T : IPagingConnection
    {
        public PagedList(
            Expression expression,
            LambdaExpression selector)
        {
            Expression = expression;
            Selector = selector;
        }

        public Expression Expression { get; }
        public LambdaExpression Selector { get; }
    }
}
