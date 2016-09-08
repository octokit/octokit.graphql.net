using System;
using System.Linq;

namespace Octoqit
{
    public class SearchResultItemConnection
    {
        public int EdgeCount { get; }
        public IQueryable<SearchResultItemEdge> Edges { get; }
        public int IssueCount { get; }
        public int RepositoryCount { get; }
        public int UserCount { get; }
    }
}
