namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A request for a user to review a pull request.
    /// </summary>
    public class ReviewRequest : QueryableValue<ReviewRequest>
    {
        public ReviewRequest(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        [Obsolete(@"Exposed database IDs will eventually be removed in favor of global Relay IDs. Use `Node.id` instead. Removal on 2018-07-01 UTC.")]
        public int? DatabaseId { get; }

        public ID Id { get; }

        /// <summary>
        /// Identifies the pull request associated with this review request.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// The reviewer that is requested.
        /// </summary>
        public RequestedReviewer RequestedReviewer => this.CreateProperty(x => x.RequestedReviewer, Octokit.GraphQL.Model.RequestedReviewer.Create);

        /// <summary>
        /// Identifies the author associated with this review request.
        /// </summary>
        [Obsolete(@"Field `reviewer` will be changed in favor of returning a union type. Use `ReviewRequest.requestedReviewer` instead. Removal on 2018-07-01 UTC.")]
        public User Reviewer => this.CreateProperty(x => x.Reviewer, Octokit.GraphQL.Model.User.Create);

        internal static ReviewRequest Create(Expression expression)
        {
            return new ReviewRequest(expression);
        }
    }
}