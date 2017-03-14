namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// A repository protected branch.
    /// </summary>
    public class ProtectedBranch : QueryEntity
    {
        public ProtectedBranch(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Check if authorizing actors for pushing is turned on.
        /// </summary>
        public bool AuthorizedActorsOnly { get; }

        /// <summary>
        /// Check if authorizing dismissers is turned on.
        /// </summary>
        public bool AuthorizedDismissalActorsOnly { get; }

        /// <summary>
        /// Check to see what level deletion blocking is being enforced.
        /// </summary>
        public int BlockDeletionsEnforcementLevel { get; }

        /// <summary>
        /// Check to see what level force pushing is being enfored.
        /// </summary>
        public int BlockForcePushesEnforcementLevel { get; }

        /// <summary>
        /// The creator of this protected branch.
        /// </summary>
        public User Creator => this.CreateProperty(x => x.Creator, Octoqit.User.Create);

        public string Id { get; }

        /// <summary>
        /// Identifies the name of the protected branch.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Check to see what level reviews are being enforced.
        /// </summary>
        public int PullRequestReviewsEnforcementLevel { get; }

        /// <summary>
        /// The repository associated with this protected branch.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        /// <summary>
        /// Check to see what level merge statuses are being enforced.
        /// </summary>
        public int RequiredStatusChecksEnforcementLevel { get; }

        /// <summary>
        /// A list review dismissal allowances for this protected branch.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public ReviewDismissalAllowanceConnection ReviewDismissalAllowances(int? first, string after, int? last, string before) => this.CreateMethodCall(x => x.ReviewDismissalAllowances(first, after, last, before), Octoqit.ReviewDismissalAllowanceConnection.Create);

        /// <summary>
        /// Check if required status checks is turned on.
        /// </summary>
        public bool StrictRequiredStatusChecksPolicy { get; }

        internal static ProtectedBranch Create(IQueryProvider provider, Expression expression)
        {
            return new ProtectedBranch(provider, expression);
        }
    }
}