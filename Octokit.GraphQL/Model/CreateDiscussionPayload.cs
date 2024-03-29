namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of CreateDiscussion
    /// </summary>
    public class CreateDiscussionPayload : QueryableValue<CreateDiscussionPayload>
    {
        internal CreateDiscussionPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The discussion that was just created.
        /// </summary>
        public Discussion Discussion => this.CreateProperty(x => x.Discussion, Octokit.GraphQL.Model.Discussion.Create);

        internal static CreateDiscussionPayload Create(Expression expression)
        {
            return new CreateDiscussionPayload(expression);
        }
    }
}