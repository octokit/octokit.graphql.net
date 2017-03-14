namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class RefEdge : QueryEntity
    {
        public RefEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Ref Node => this.CreateProperty(x => x.Node, Octoqit.Ref.Create);

        internal static RefEdge Create(IQueryProvider provider, Expression expression)
        {
            return new RefEdge(provider, expression);
        }
    }
}