namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A branch protection rule.
    /// </summary>
    public class BranchProtectionRule : QueryableValue<BranchProtectionRule>
    {
        public BranchProtectionRule(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of conflicts matching branches protection rule and other branch protection rules
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public BranchProtectionRuleConflictConnection BranchProtectionRuleConflicts(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.BranchProtectionRuleConflicts(first, after, last, before), Octokit.GraphQL.Model.BranchProtectionRuleConflictConnection.Create);

        /// <summary>
        /// The actor who created this branch protection rule.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// Will new commits pushed to matching branches dismiss pull request review approvals.
        /// </summary>
        public bool DismissesStaleReviews { get; }

        public ID Id { get; }

        /// <summary>
        /// Can admins overwrite branch protection.
        /// </summary>
        public bool IsAdminEnforced { get; }

        /// <summary>
        /// Repository refs that are protected by this rule
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public RefConnection MatchingRefs(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.MatchingRefs(first, after, last, before), Octokit.GraphQL.Model.RefConnection.Create);

        /// <summary>
        /// Identifies the protection rule pattern.
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        /// A list push allowances for this branch protection rule.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PushAllowanceConnection PushAllowances(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.PushAllowances(first, after, last, before), Octokit.GraphQL.Model.PushAllowanceConnection.Create);

        /// <summary>
        /// The repository associated with this branch protection rule.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Number of approving reviews required to update matching branches.
        /// </summary>
        public int? RequiredApprovingReviewCount { get; }

        /// <summary>
        /// List of required status check contexts that must pass for commits to be accepted to matching branches.
        /// </summary>
        public IEnumerable<string> RequiredStatusCheckContexts { get; }

        /// <summary>
        /// Are approving reviews required to update matching branches.
        /// </summary>
        public bool RequiresApprovingReviews { get; }

        /// <summary>
        /// Are commits required to be signed.
        /// </summary>
        public bool RequiresCommitSignatures { get; }

        /// <summary>
        /// Are status checks required to update matching branches.
        /// </summary>
        public bool RequiresStatusChecks { get; }

        /// <summary>
        /// Are branches required to be up to date before merging.
        /// </summary>
        public bool RequiresStrictStatusChecks { get; }

        /// <summary>
        /// Is pushing to matching branches restricted.
        /// </summary>
        public bool RestrictsPushes { get; }

        /// <summary>
        /// Is dismissal of pull request reviews restricted.
        /// </summary>
        public bool RestrictsReviewDismissals { get; }

        /// <summary>
        /// A list review dismissal allowances for this branch protection rule.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ReviewDismissalAllowanceConnection ReviewDismissalAllowances(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.ReviewDismissalAllowances(first, after, last, before), Octokit.GraphQL.Model.ReviewDismissalAllowanceConnection.Create);

        internal static BranchProtectionRule Create(Expression expression)
        {
            return new BranchProtectionRule(expression);
        }
    }
}