namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A team or user who has the ability to dismiss a review on a protected branch.
    /// </summary>
    public class ReviewDismissalAllowance : QueryableValue<ReviewDismissalAllowance>
    {
        public ReviewDismissalAllowance(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The actor that can dismiss.
        /// </summary>
        public ReviewDismissalAllowanceActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.ReviewDismissalAllowanceActor.Create);

        /// <summary>
        /// Identifies the branch protection rule associated with the allowed user or team.
        /// </summary>
        public BranchProtectionRule BranchProtectionRule => this.CreateProperty(x => x.BranchProtectionRule, Octokit.GraphQL.Model.BranchProtectionRule.Create);

        public ID Id { get; }

        /// <summary>
        /// Identifies the protected branch associated with the allowed user or team.
        /// </summary>
        [Obsolete(@"The `ProtectedBranch` type is deprecated and will be removed soon. Use `ReviewDismissalAllowance.branchProtectionRule` instead. Removal on 2019-01-01 UTC.")]
        public ProtectedBranch ProtectedBranch => this.CreateProperty(x => x.ProtectedBranch, Octokit.GraphQL.Model.ProtectedBranch.Create);

        internal static ReviewDismissalAllowance Create(Expression expression)
        {
            return new ReviewDismissalAllowance(expression);
        }
    }
}