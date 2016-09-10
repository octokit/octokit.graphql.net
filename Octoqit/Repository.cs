using System;
using System.Linq;
using LinqToGraphQL;

namespace Octoqit
{
    public class Repository :  ISearchResultItem
    {
        public string Id { get; }
        public string Name { get; }
        public IQueryable<Owner> Owner { get; }
        public bool IsFork { get; }
        public bool IsPrivate { get; }
    }
}
