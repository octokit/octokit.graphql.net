namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An object that can be locked.
    /// </summary>
    public interface ILockable : IQueryableValue<ILockable>, IQueryableInterface
    {
        /// <summary>
        /// Reason that the conversation was locked.
        /// </summary>
        LockReason? ActiveLockReason { get; }

        /// <summary>
        /// `true` if the object is locked
        /// </summary>
        bool Locked { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubILockable : QueryableValue<StubILockable>, ILockable
    {
        internal StubILockable(Expression expression) : base(expression)
        {
        }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public LockReason? ActiveLockReason { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public bool Locked { get; }

        internal static StubILockable Create(Expression expression)
        {
            return new StubILockable(expression);
        }
    }
}