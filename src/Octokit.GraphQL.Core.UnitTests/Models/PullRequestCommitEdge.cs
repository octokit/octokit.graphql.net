using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class PullRequestCommitEdge : QueryableValue<PullRequestCommitEdge>
    {
        public PullRequestCommitEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public PullRequestCommit Node => this.CreateProperty(x => x.Node, PullRequestCommit.Create);

        internal static PullRequestCommitEdge Create(Expression expression)
        {
            return new PullRequestCommitEdge(expression);
        }
    }
}