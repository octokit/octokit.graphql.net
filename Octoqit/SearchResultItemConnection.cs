using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;

namespace Octoqit
{
    public class SearchResultItemConnection : QueryEntity
    {
        public SearchResultItemConnection(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public int EdgeCount { get; }
        public IQueryable<SearchResultItemEdge> Edges { get; }
        public int IssueCount { get; }
        public int RepositoryCount { get; }
        public int UserCount { get; }
    }
}
