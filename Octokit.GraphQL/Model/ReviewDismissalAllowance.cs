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

        public string Id { get; }

        /// <summary>
        /// Identifies the protected branch associated with the allowed user or team.
        /// </summary>
        public ProtectedBranch ProtectedBranch => this.CreateProperty(x => x.ProtectedBranch, Octokit.GraphQL.Model.ProtectedBranch.Create);

        internal static ReviewDismissalAllowance Create(Expression expression)
        {
            return new ReviewDismissalAllowance(expression);
        }
    }
}