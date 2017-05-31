namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'review_requested' event on a given pull request.
    /// </summary>
    public class ReviewRequestedEvent : QueryEntity
    {
        public ReviewRequestedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the 'base_ref_force_pushed' event.
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
        /// Identifies the user whose review was requested.
        /// </summary>
        public User Subject => this.CreateProperty(x => x.Subject, Octokit.GraphQL.User.Create);

        internal static ReviewRequestedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new ReviewRequestedEvent(provider, expression);
        }
    }
}