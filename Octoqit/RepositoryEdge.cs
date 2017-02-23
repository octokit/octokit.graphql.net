using System;
using System.Linq;
using LinqToGraphQL;

namespace Octoqit
{
    public class RepositoryEdge : QueryEntity
    {
        public RepositoryEdge(IQueryProvider provider)
            : base(provider)
        {
        }

        public string Cursor { get; }
        public Repository Node { get; }
    }
}
