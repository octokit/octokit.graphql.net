using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class PageInfo : QueryableValue<PageInfo>
    {
        public PageInfo(Expression expression) : base(expression)
        {
        }

        public string EndCursor { get; }
        public bool HasNextPage { get; }
        public bool HasPreviousPage { get; }
        public string StartCursor { get; }

        internal static PageInfo Create(Expression expression)
        {
            return new PageInfo(expression);
        }
    }
}