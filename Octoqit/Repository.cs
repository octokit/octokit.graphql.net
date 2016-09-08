using System;
using System.Linq;

namespace Octoqit
{
    public class Repository : ISearchResultItem
    {
        public string Id { get; }
        public string Name { get; }
        public IQueryable<Owner> Owner { get; }
        public bool IsFork { get; }
        public bool IsPrivate { get; }
        public IQueryable<Star> Stars { get; }
    }
}
