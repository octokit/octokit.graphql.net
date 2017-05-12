namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class ReactionEdge : QueryEntity
    {
        public ReactionEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Reaction Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Reaction.Create);

        internal static ReactionEdge Create(IQueryProvider provider, Expression expression)
        {
            return new ReactionEdge(provider, expression);
        }
    }
}