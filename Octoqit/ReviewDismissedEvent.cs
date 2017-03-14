namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a 'review_dismissed' event on a given issue or pull request.
    /// </summary>
    public class ReviewDismissedEvent : QueryEntity
    {
        public ReviewDismissedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor (user) associated with the event.
        /// </summary>
        public User Actor => this.CreateProperty(x => x.Actor, Octoqit.User.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the issue associated with the event.
        /// </summary>
        public Issue Issue => this.CreateProperty(x => x.Issue, Octoqit.Issue.Create);

        /// <summary>
        /// Identifies the message associated with the 'review_dismissed' event.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Identifies the previous state of the review with the 'review_dismissed' event.
        /// </summary>
        public PullRequestReviewState PreviousReviewState { get; }

        /// <summary>
        /// Identifies the repository associated with the event.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        /// <summary>
        /// Identifies the review associated with the 'review_dismissed' event.
        /// </summary>
        public PullRequestReview Review => this.CreateProperty(x => x.Review, Octoqit.PullRequestReview.Create);

        /// <summary>
        /// Identifies the event type associated with the event.
        /// </summary>
        public IssueEventType Type { get; }

        internal static ReviewDismissedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new ReviewDismissedEvent(provider, expression);
        }
    }
}