namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'review_request_removed' event on a given pull request.
    /// </summary>
    public class ReviewRequestRemovedEvent : QueryableValue<ReviewRequestRemovedEvent>
    {
        /// <inheritdoc />
        public ReviewRequestRemovedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        public ID Id { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// Identifies the reviewer whose review request was removed.
        /// </summary>
        public RequestedReviewer RequestedReviewer => this.CreateProperty(x => x.RequestedReviewer, Octokit.GraphQL.Model.RequestedReviewer.Create);

        /// <summary>
        /// Identifies the user whose review request was removed.
        /// </summary>
        [Obsolete(@"`subject` will be renamed. Use `ReviewRequestRemovedEvent.requestedReviewer` instead. Removal on 2018-07-01 UTC.")]
        public User Subject => this.CreateProperty(x => x.Subject, Octokit.GraphQL.Model.User.Create);

        internal static ReviewRequestRemovedEvent Create(Expression expression)
        {
            return new ReviewRequestRemovedEvent(expression);
        }
    }
}