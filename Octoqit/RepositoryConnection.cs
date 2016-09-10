using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;

namespace Octoqit
{
    public class RepositoryConnection : QueryEntity
    {
        public RepositoryConnection(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public IQueryable<RepositoryEdge> Edges
        {
            get { return Property<RepositoryEdge>(nameof(Edges)); }
        }

        public int TotalCount { get; }
    }
}
