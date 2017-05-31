namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A team or user who has the ability to push to a protected branch.
    /// </summary>
    public class PushAllowance : QueryEntity
    {
        public PushAllowance(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The actor that can push.
        /// </summary>
        public PushAllowanceActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.PushAllowanceActor.Create);

        public string Id { get; }

        /// <summary>
        /// Identifies the protected branch associated with the allowed user or team.
        /// </summary>
        public ProtectedBranch ProtectedBranch => this.CreateProperty(x => x.ProtectedBranch, Octokit.GraphQL.ProtectedBranch.Create);

        internal static PushAllowance Create(IQueryProvider provider, Expression expression)
        {
            return new PushAllowance(provider, expression);
        }
    }
}