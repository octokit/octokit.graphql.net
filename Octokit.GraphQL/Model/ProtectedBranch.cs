namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository protected branch.
    /// </summary>
    public class ProtectedBranch : QueryableValue<ProtectedBranch>
    {
        public ProtectedBranch(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The actor who created this protected branch.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Will new commits pushed to this branch dismiss pull request review approvals.
        /// </summary>
        public bool HasDismissableStaleReviews { get; }

        /// <summary>
        /// Are reviews required to update this branch.
        /// </summary>
        public bool HasRequiredReviews { get; }

        /// <summary>
        /// Are status checks required to update this branch.
        /// </summary>
        public bool HasRequiredStatusChecks { get; }

        /// <summary>
        /// Is pushing to this branch restricted.
        /// </summary>
        public bool HasRestrictedPushes { get; }

        /// <summary>
        /// Is dismissal of pull request reviews restricted.
        /// </summary>
        public bool HasRestrictedReviewDismissals { get; }

        /// <summary>
        /// Are branches required to be up to date before merging.
        /// </summary>
        public bool HasStrictRequiredStatusChecks { get; }

        public string Id { get; }

        /// <summary>
        /// Can admins overwrite branch protection.
        /// </summary>
        public bool IsAdminEnforced { get; }

        /// <summary>
        /// Identifies the name of the protected branch.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A list push allowances for this protected branch.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public PushAllowanceConnection PushAllowances(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.PushAllowances(first, after, last, before), Octokit.GraphQL.Model.PushAllowanceConnection.Create);

        /// <summary>
        /// The repository associated with this protected branch.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// List of required status check contexts that must pass for commits to be accepted to this branch.
        /// </summary>
        public IEnumerable<string> RequiredStatusCheckContexts { get; }

        /// <summary>
        /// A list review dismissal allowances for this protected branch.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public ReviewDismissalAllowanceConnection ReviewDismissalAllowances(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.ReviewDismissalAllowances(first, after, last, before), Octokit.GraphQL.Model.ReviewDismissalAllowanceConnection.Create);

        internal static ProtectedBranch Create(Expression expression)
        {
            return new ProtectedBranch(expression);
        }
    }
}