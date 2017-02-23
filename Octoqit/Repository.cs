using System;
using System.Linq;
using LinqToGraphQL;

namespace Octoqit
{
    public class Repository : QueryEntity, ISearchResultItem
    {
        public Repository(IQueryProvider provider)
            : base(provider)
        {
        }

        public string Id { get; }
        public string Name { get; }
        public IQueryable<RepositoryOwner> Owner { get; }
        public bool IsFork { get; }
        public bool IsPrivate { get; }
    }
}
