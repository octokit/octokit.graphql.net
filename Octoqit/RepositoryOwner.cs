using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;
using LinqToGraphQL.Builders;

namespace Octoqit
{
    public class RepositoryOwner : QueryEntity
    {
        public string Id { get; }
        public string Login { get; }

        [GraphQLIdentifier("avatarURL")]
        public string AvatarUrl { get; }

        public RepositoryOwner(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public RepositoryConnection Repositories(
            int? first = null,
            string after = null,
            int? last = null,
            string before = null)
        {
            return this.CreateMethodCall(x => x.Repositories(first, after, last, before), RepositoryConnection.Create);
        }

        public IQueryable<Repository> Repository(string name)
        {
            return this.CreateMethodCall(x => x.Repository(name));
        }

        internal static RepositoryOwner Create(IQueryProvider provider, Expression expression)
        {
            return new RepositoryOwner(provider, expression);
        }
    }
}
