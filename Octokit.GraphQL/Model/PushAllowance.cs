namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A team or user who has the ability to push to a protected branch.
    /// </summary>
    public class PushAllowance : QueryableValue<PushAllowance>
    {
        /// <inheritdoc />
        public PushAllowance(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The actor that can push.
        /// </summary>
        public PushAllowanceActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.PushAllowanceActor.Create);

        public ID Id { get; }

        /// <summary>
        /// Identifies the protected branch associated with the allowed user or team.
        /// </summary>
        public ProtectedBranch ProtectedBranch => this.CreateProperty(x => x.ProtectedBranch, Octokit.GraphQL.Model.ProtectedBranch.Create);

        internal static PushAllowance Create(Expression expression)
        {
            return new PushAllowance(expression);
        }
    }
}