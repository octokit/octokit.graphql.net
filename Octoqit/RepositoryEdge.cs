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
            Node = this.CreateProperty(x => x.Node, Repository.Create);
        }

        public string Cursor { get; }
        public Repository Node { get; }
    }
}
