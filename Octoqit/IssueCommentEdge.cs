namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class IssueCommentEdge : QueryEntity
    {
        public IssueCommentEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public IssueComment Node => this.CreateProperty(x => x.Node, Octoqit.IssueComment.Create);

        internal static IssueCommentEdge Create(IQueryProvider provider, Expression expression)
        {
            return new IssueCommentEdge(provider, expression);
        }
    }
}