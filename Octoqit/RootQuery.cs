using System;
using System.Linq;
using LinqToGraphQL;

namespace Octoqit
{
    public class RootQuery : IRootQuery
    {
        public IQueryable<RepositoryOwner> RepositoryOwner(string login)
        {
            throw new NotImplementedException();
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
