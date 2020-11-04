namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A ref update rules for a viewer.
    /// </summary>
    public class RefUpdateRule : QueryableValue<RefUpdateRule>
    {
        internal RefUpdateRule(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Can this branch be deleted.
        /// </summary>
        public bool AllowsDeletions { get; }

        /// <summary>
        /// Are force pushes allowed on this branch.
        /// </summary>
        public bool AllowsForcePushes { get; }

        /// <summary>
        /// Identifies the protection rule pattern.
        /// </summary>
        public string Pattern { get; }

        /// <summary>
        /// Number of approving reviews required to update matching branches.
        /// </summary>
        public int? RequiredApprovingReviewCount { get; }

        /// <summary>
        /// List of required status check contexts that must pass for commits to be accepted to matching branches.
        /// </summary>
        public IEnumerable<string> RequiredStatusCheckContexts { get; }

        /// <summary>
        /// Are merge commits prohibited from being pushed to this branch.
        /// </summary>
        public bool RequiresLinearHistory { get; }

        /// <summary>
        /// Are commits required to be signed.
        /// </summary>
        public bool RequiresSignatures { get; }

        /// <summary>
        /// Can the viewer push to the branch
        /// </summary>
        public bool ViewerCanPush { get; }

        internal static RefUpdateRule Create(Expression expression)
        {
            return new RefUpdateRule(expression);
        }
    }
}