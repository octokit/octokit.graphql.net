namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class ReviewDismissalAllowanceEdge : QueryEntity
    {
        public ReviewDismissalAllowanceEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ReviewDismissalAllowance Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.ReviewDismissalAllowance.Create);

        internal static ReviewDismissalAllowanceEdge Create(IQueryProvider provider, Expression expression)
        {
            return new ReviewDismissalAllowanceEdge(provider, expression);
        }
    }
}