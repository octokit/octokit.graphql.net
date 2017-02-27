using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;
using LinqToGraphQL.Builders;

namespace Octoqit
{
    public class SearchResultItemConnection : QueryEntity
    {
        public SearchResultItemConnection(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
            Edges = this.CreateProperty(x => x.Edges);
        }

        public int EdgeCount { get; }
        public IQueryable<SearchResultItemEdge> Edges { get; }
        public int IssueCount { get; }
        public int RepositoryCount { get; }
        public int UserCount { get; }

        internal static SearchResultItemConnection Create(IQueryProvider provider, Expression expression)
        {
            return new SearchResultItemConnection(provider, expression);
        }
    }
}
