namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of LockLockable
    /// </summary>
    public class LockLockablePayload : QueryableValue<LockLockablePayload>
    {
        internal LockLockablePayload(Expression expression) : base(expression)
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
        /// The item that was locked.
        /// </summary>
        public ILockable LockedRecord => this.CreateProperty(x => x.LockedRecord, Octokit.GraphQL.Model.Internal.StubILockable.Create);

        internal static LockLockablePayload Create(Expression expression)
        {
            return new LockLockablePayload(expression);
        }
    }
}