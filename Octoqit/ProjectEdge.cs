namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class ProjectEdge : QueryEntity
    {
        public ProjectEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Project Node => this.CreateProperty(x => x.Node, Octoqit.Project.Create);

        internal static ProjectEdge Create(IQueryProvider provider, Expression expression)
        {
            return new ProjectEdge(provider, expression);
        }
    }
}