using System;
using System.Linq;
using LinqToGraphQL;
using Exp = System.Linq.Expressions.Expression;

namespace Octoqit
{
    public class RootQuery : QueryEntity, IRootQuery
    {
        public RootQuery()
            : base(new QueryProvider())
        {
            Viewer = new User(Provider, Exp.Property(Expression, nameof(Viewer)));
        }

        public User Viewer { get; }

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
            return new SearchResultItemConnection(Provider, MethodCallExpression(nameof(Search), Arg(type), Arg(query), Arg(first), Arg(after), Arg(last), Arg(before)));
        }
    }
}
