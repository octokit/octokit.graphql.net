using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;
using LinqToGraphQL.Builders;

namespace Octoqit
{
    public class RepositoryConnection : QueryEntity
    {
        public RepositoryConnection(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
            Edges = this.CreateProperty(x => x.Edges);
        }

        public IQueryable<RepositoryEdge> Edges { get; }

        public int TotalCount { get; }

        internal static RepositoryConnection Create(IQueryProvider provider, Expression expression)
        {
            return new RepositoryConnection(provider, expression);
        }
    }
}
