namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class ProtectedBranchEdge : QueryEntity
    {
        public ProtectedBranchEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ProtectedBranch Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.ProtectedBranch.Create);

        internal static ProtectedBranchEdge Create(IQueryProvider provider, Expression expression)
        {
            return new ProtectedBranchEdge(provider, expression);
        }
    }
}