namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class ReviewRequestEdge : QueryEntity
    {
        public ReviewRequestEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ReviewRequest Node => this.CreateProperty(x => x.Node, Octoqit.ReviewRequest.Create);

        internal static ReviewRequestEdge Create(IQueryProvider provider, Expression expression)
        {
            return new ReviewRequestEdge(provider, expression);
        }
    }
}