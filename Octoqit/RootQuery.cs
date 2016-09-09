using System;
using System.Linq;
using LinqToGraphQL;

namespace Octoqit
{
    public class RootQuery : QueryEntity, IRootQuery
    {
        public RootQuery()
            : base(new QueryProvider())
        {
        }

        public RepositoryOwner RepositoryOwner(string login)
        {
            return new RepositoryOwner(Provider, MethodCallExpression(nameof(RepositoryOwner), Arg(login)));
        }

        public SearchResultItemConnection Search(
            SearchType type,
            string query,
            int? first = null,
            string after = null,
            int? last = null,
            string before = null)
        {
            throw new NotImplementedException();
        }
    }
}
