using System;
using System.Linq;
using LinqToGraphQL;
using LinqToGraphQL.Builders;

namespace Octoqit
{
    public class RepositoryEdge : QueryEntity
    {
        public RepositoryEdge(IQueryProvider provider)
            : base(provider)
        {
        }

        public string Cursor { get; }
        public Repository Node => this.CreateProperty(x => x.Node, Repository.Create);
    }
}
