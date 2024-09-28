using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class PullRequestTimelineItem : QueryableValue<PullRequestTimelineItem>, IUnion
    {
        public PullRequestTimelineItem(Expression expression) : base(expression)
        {
        }

        public TResult Switch<TResult>(Func<Selector<TResult>, Selector<TResult>> select) => default;

        public class Selector<T>
        {
            public Selector<T> Commit(Func<Commit, T> selector) => default;
            public Selector<T> IssueComment(Func<IssueComment, T> selector) => default;
        }

        internal static PullRequestTimelineItem Create(Expression expression)
        {
            return new PullRequestTimelineItem(expression);
        }
    }
}