namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class IssueEdge : QueryEntity
    {
        public IssueEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Issue Node => this.CreateProperty(x => x.Node, Octoqit.Issue.Create);

        internal static IssueEdge Create(IQueryProvider provider, Expression expression)
        {
            return new IssueEdge(provider, expression);
        }
    }
}