namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of DeleteProjectV2Item.
    /// </summary>
    public class DeleteProjectV2ItemPayload : QueryableValue<DeleteProjectV2ItemPayload>
    {
        internal DeleteProjectV2ItemPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The ID of the deleted item.
        /// </summary>
        public ID? DeletedItemId { get; }

        internal static DeleteProjectV2ItemPayload Create(Expression expression)
        {
            return new DeleteProjectV2ItemPayload(expression);
        }
    }
}