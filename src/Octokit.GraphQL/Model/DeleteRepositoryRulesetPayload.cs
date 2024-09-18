namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of DeleteRepositoryRuleset.
    /// </summary>
    public class DeleteRepositoryRulesetPayload : QueryableValue<DeleteRepositoryRulesetPayload>
    {
        internal DeleteRepositoryRulesetPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        internal static DeleteRepositoryRulesetPayload Create(Expression expression)
        {
            return new DeleteRepositoryRulesetPayload(expression);
        }
    }
}