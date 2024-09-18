namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of AbortQueuedMigrations.
    /// </summary>
    public class AbortQueuedMigrationsPayload : QueryableValue<AbortQueuedMigrationsPayload>
    {
        internal AbortQueuedMigrationsPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// Did the operation succeed?
        /// </summary>
        public bool? Success { get; }

        internal static AbortQueuedMigrationsPayload Create(Expression expression)
        {
            return new AbortQueuedMigrationsPayload(expression);
        }
    }
}