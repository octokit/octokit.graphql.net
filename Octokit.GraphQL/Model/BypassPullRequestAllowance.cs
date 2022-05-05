namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A team or user who has the ability to bypass a pull request requirement on a protected branch.
    /// </summary>
    public class BypassPullRequestAllowance : QueryableValue<BypassPullRequestAllowance>
    {
        internal BypassPullRequestAllowance(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The actor that can dismiss.
        /// </summary>
        public BranchActorAllowanceActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.BranchActorAllowanceActor.Create);

        /// <summary>
        /// Identifies the branch protection rule associated with the allowed user or team.
        /// </summary>
        public BranchProtectionRule BranchProtectionRule => this.CreateProperty(x => x.BranchProtectionRule, Octokit.GraphQL.Model.BranchProtectionRule.Create);

        public ID Id { get; }

        internal static BypassPullRequestAllowance Create(Expression expression)
        {
            return new BypassPullRequestAllowance(expression);
        }
    }
}