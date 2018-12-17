namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A request for a user to review a pull request.
    /// </summary>
    public class ReviewRequest : QueryableValue<ReviewRequest>
    {
        internal ReviewRequest(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public ID Id { get; }

        /// <summary>
        /// Identifies the pull request associated with this review request.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// The reviewer that is requested.
        /// </summary>
        public RequestedReviewer RequestedReviewer => this.CreateProperty(x => x.RequestedReviewer, Octokit.GraphQL.Model.RequestedReviewer.Create);

        internal static ReviewRequest Create(Expression expression)
        {
            return new ReviewRequest(expression);
        }
    }
}