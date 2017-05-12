namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class PullRequestReviewCommentEdge : QueryEntity
    {
        public PullRequestReviewCommentEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public PullRequestReviewComment Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.PullRequestReviewComment.Create);

        internal static PullRequestReviewCommentEdge Create(IQueryProvider provider, Expression expression)
        {
            return new PullRequestReviewCommentEdge(provider, expression);
        }
    }
}