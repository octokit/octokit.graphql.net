namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'review_request_removed' event on a given pull request.
    /// </summary>
    public class ReviewRequestRemovedEvent : QueryEntity
    {
        public ReviewRequestRemovedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// PullRequest referenced by event.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.PullRequest.Create);

        /// <summary>
        /// Identifies the user whose review request was removed.
        /// </summary>
        public User Subject => this.CreateProperty(x => x.Subject, Octokit.GraphQL.User.Create);

        internal static ReviewRequestRemovedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new ReviewRequestRemovedEvent(provider, expression);
        }
    }
}