using System;
using System.Linq;
using LinqToGraphQL;

namespace Octoqit
{
    public class SearchResultItemEdge : QueryEntity
    {
        public SearchResultItemEdge(IQueryProvider provider)
            : base(provider)
        {
        }

        public string Cursor { get; }
        public ISearchResultItem Node { get; }
    }
}
