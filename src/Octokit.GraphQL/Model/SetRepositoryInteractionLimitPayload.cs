namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of SetRepositoryInteractionLimit.
    /// </summary>
    public class SetRepositoryInteractionLimitPayload : QueryableValue<SetRepositoryInteractionLimitPayload>
    {
        internal SetRepositoryInteractionLimitPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The repository that the interaction limit was set for.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static SetRepositoryInteractionLimitPayload Create(Expression expression)
        {
            return new SetRepositoryInteractionLimitPayload(expression);
        }
    }
}