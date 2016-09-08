using System;
using System.Linq;

namespace Octoqit
{
    public class RepositoryOwner
    {
        public string Id { get; }
        public string Login { get; }
        public string AvatarUrl { get; }

        public RepositoryConnection Repositories(
            int? first = null,
            string after = null,
            int? last = null,
            string before = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Repository> Repository(string name)
        {
            throw new NotImplementedException();
        }
    }
}
