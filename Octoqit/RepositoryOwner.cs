using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL;

namespace Octoqit
{
    public class RepositoryOwner : QueryEntity
    {
        public string Id { get; }
        public string Login { get; }
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
            return new RepositoryConnection(
                Provider,
                MethodCallExpression(nameof(Repositories), Arg(first), Arg(after), Arg(last), Arg(before)));
        }

        public IQueryable<Repository> Repository(string name)
        {
            return MethodCall<Repository>(nameof(Repository), Arg(name));
        }
    }
}
