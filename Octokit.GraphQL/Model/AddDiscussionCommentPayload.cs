namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of AddDiscussionComment
    /// </summary>
    public class AddDiscussionCommentPayload : QueryableValue<AddDiscussionCommentPayload>
    {
        internal AddDiscussionCommentPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The newly created discussion comment.
        /// </summary>
        public DiscussionComment Comment => this.CreateProperty(x => x.Comment, Octokit.GraphQL.Model.DiscussionComment.Create);

        internal static AddDiscussionCommentPayload Create(Expression expression)
        {
            return new AddDiscussionCommentPayload(expression);
        }
    }
}