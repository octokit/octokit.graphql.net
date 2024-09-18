namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of AddPullRequestReviewComment.
    /// </summary>
    public class AddPullRequestReviewCommentPayload : QueryableValue<AddPullRequestReviewCommentPayload>
    {
        internal AddPullRequestReviewCommentPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The newly created comment.
        /// </summary>
        public PullRequestReviewComment Comment => this.CreateProperty(x => x.Comment, Octokit.GraphQL.Model.PullRequestReviewComment.Create);

        /// <summary>
        /// The edge from the review's comment connection.
        /// </summary>
        public PullRequestReviewCommentEdge CommentEdge => this.CreateProperty(x => x.CommentEdge, Octokit.GraphQL.Model.PullRequestReviewCommentEdge.Create);

        internal static AddPullRequestReviewCommentPayload Create(Expression expression)
        {
            return new AddPullRequestReviewCommentPayload(expression);
        }
    }
}