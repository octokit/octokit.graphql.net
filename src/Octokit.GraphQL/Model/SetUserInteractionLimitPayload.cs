namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of SetUserInteractionLimit.
    /// </summary>
    public class SetUserInteractionLimitPayload : QueryableValue<SetUserInteractionLimitPayload>
    {
        internal SetUserInteractionLimitPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The user that the interaction limit was set for.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static SetUserInteractionLimitPayload Create(Expression expression)
        {
            return new SetUserInteractionLimitPayload(expression);
        }
    }
}