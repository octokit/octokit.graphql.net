namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'review_dismissed' event on a given issue or pull request.
    /// </summary>
    public class ReviewDismissedEvent : QueryEntity
    {
        public ReviewDismissedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the 'review_dismissed' event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the message associated with the 'review_dismissed' event.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Identifies the previous state of the review with the 'review_dismissed' event.
        /// </summary>
        public PullRequestReviewState PreviousReviewState { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.PullRequest.Create);

        /// <summary>
        /// Identifies the review associated with the 'review_dismissed' event.
        /// </summary>
        public PullRequestReview Review => this.CreateProperty(x => x.Review, Octokit.GraphQL.PullRequestReview.Create);

        internal static ReviewDismissedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new ReviewDismissedEvent(provider, expression);
        }
    }
}