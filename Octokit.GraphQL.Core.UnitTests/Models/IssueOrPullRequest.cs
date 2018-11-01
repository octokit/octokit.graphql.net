using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class IssueOrPullRequest : QueryableValue<IssueOrPullRequest>, IUnion
    {
        public IssueOrPullRequest(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Expression<Func<Selector<TResult>, Selector<TResult>>> select) => default;

        public class Selector<T>
        {
            public Selector<T> Issue(Func<Issue, T> selector) => default;
            public Selector<T> PullRequest(Func<PullRequest, T> selector) => default;
        }

        internal static IssueOrPullRequest Create(Expression expression)
        {
            return new IssueOrPullRequest(expression);
        }
    }
}