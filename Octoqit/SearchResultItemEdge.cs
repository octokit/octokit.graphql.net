using System;

namespace Octoqit
{
    public class SearchResultItemEdge
    {
        public string Cursor { get; }
        public ISearchResultItem Node { get; }
    }
}
