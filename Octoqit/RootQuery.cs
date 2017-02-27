using System;
using LinqToGraphQL;
using LinqToGraphQL.Builders;

namespace Octoqit
{
    public class RootQuery : QueryEntity, IRootQuery
    {
        public RootQuery()
            : base(new QueryProvider())
        {
        }

        public User Viewer => this.CreateProperty(x => x.Viewer, User.Create);

        public RepositoryOwner RepositoryOwner(string login)
        {
            return this.CreateMethodCall(x => x.RepositoryOwner(login), Octoqit.RepositoryOwner.Create);
        }

        public SearchResultItemConnection Search(
            SearchType type,
            string query,
            int? first = null,
            string after = null,
            int? last = null,
            string before = null)
        {
            return this.CreateMethodCall(x => x.Search(type, query, first, after, last, before), SearchResultItemConnection.Create);
        }
    }
}
