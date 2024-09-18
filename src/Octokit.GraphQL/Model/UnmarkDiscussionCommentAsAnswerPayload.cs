namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of UnmarkDiscussionCommentAsAnswer.
    /// </summary>
    public class UnmarkDiscussionCommentAsAnswerPayload : QueryableValue<UnmarkDiscussionCommentAsAnswerPayload>
    {
        internal UnmarkDiscussionCommentAsAnswerPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The discussion that includes the comment.
        /// </summary>
        public Discussion Discussion => this.CreateProperty(x => x.Discussion, Octokit.GraphQL.Model.Discussion.Create);

        internal static UnmarkDiscussionCommentAsAnswerPayload Create(Expression expression)
        {
            return new UnmarkDiscussionCommentAsAnswerPayload(expression);
        }
    }
}