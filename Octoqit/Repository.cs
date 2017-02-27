using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;

namespace Octoqit
{
    public class Repository : QueryEntity, ISearchResultItem
    {
        public Repository(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public string Id { get; }
        public string Name { get; }
        public IQueryable<RepositoryOwner> Owner { get; }
        public bool IsFork { get; }
        public bool IsPrivate { get; }

        internal static Repository Create(IQueryProvider provider, Expression expression)
        {
            return new Repository(provider, expression);
        }
    }
}
