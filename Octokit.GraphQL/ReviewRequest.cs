namespace Octokit.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A request for a user to review a pull request.
    /// </summary>
    public class ReviewRequest : QueryEntity
    {
        public ReviewRequest(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the pull request associated with this review request.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.PullRequest.Create);

        /// <summary>
        /// Identifies the author associated with this review request.
        /// </summary>
        public User Reviewer => this.CreateProperty(x => x.Reviewer, Octokit.GraphQL.User.Create);

        internal static ReviewRequest Create(IQueryProvider provider, Expression expression)
        {
            return new ReviewRequest(provider, expression);
        }
    }
}