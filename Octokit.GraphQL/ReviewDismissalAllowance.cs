namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A team or user who as the ability to dismiss a review on a protected branch.
    /// </summary>
    public class ReviewDismissalAllowance : QueryEntity
    {
        public ReviewDismissalAllowance(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The actor that can dismiss.
        /// </summary>
        public ReviewDismissalAllowanceActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.ReviewDismissalAllowanceActor.Create);

        public string Id { get; }

        /// <summary>
        /// Identifies the protected branch associated with the allowed user or team.
        /// </summary>
        public ProtectedBranch ProtectedBranch => this.CreateProperty(x => x.ProtectedBranch, Octokit.GraphQL.ProtectedBranch.Create);

        internal static ReviewDismissalAllowance Create(IQueryProvider provider, Expression expression)
        {
            return new ReviewDismissalAllowance(provider, expression);
        }
    }
}