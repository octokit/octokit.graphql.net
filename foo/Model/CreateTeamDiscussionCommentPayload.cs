namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of CreateTeamDiscussionComment
    /// </summary>
    public class CreateTeamDiscussionCommentPayload : QueryableValue<CreateTeamDiscussionCommentPayload>
    {
        internal CreateTeamDiscussionCommentPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The new comment.
        /// </summary>
        public TeamDiscussionComment TeamDiscussionComment => this.CreateProperty(x => x.TeamDiscussionComment, Octokit.GraphQL.Model.TeamDiscussionComment.Create);

        internal static CreateTeamDiscussionCommentPayload Create(Expression expression)
        {
            return new CreateTeamDiscussionCommentPayload(expression);
        }
    }
}