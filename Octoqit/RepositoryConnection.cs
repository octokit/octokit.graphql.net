using System;
using System.Linq;

namespace Octoqit
{
    public class RepositoryConnection
    {
        public IQueryable<RepositoryEdge> Edges { get; }
        public int TotalCount { get; }
    }
}
