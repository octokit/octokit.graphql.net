namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class TeamEdge : QueryEntity
    {
        public TeamEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Team Node => this.CreateProperty(x => x.Node, Octoqit.Team.Create);

        internal static TeamEdge Create(IQueryProvider provider, Expression expression)
        {
            return new TeamEdge(provider, expression);
        }
    }
}