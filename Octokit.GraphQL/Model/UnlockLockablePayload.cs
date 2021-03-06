namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of UnlockLockable
    /// </summary>
    public class UnlockLockablePayload : QueryableValue<UnlockLockablePayload>
    {
        internal UnlockLockablePayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The item that was unlocked.
        /// </summary>
        public ILockable UnlockedRecord => this.CreateProperty(x => x.UnlockedRecord, Octokit.GraphQL.Model.Internal.StubILockable.Create);

        internal static UnlockLockablePayload Create(Expression expression)
        {
            return new UnlockLockablePayload(expression);
        }
    }
}