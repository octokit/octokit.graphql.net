namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a user that's starred a repository.
    /// </summary>
    public class StargazerEdge : QueryEntity
    {
        public StargazerEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Cursor { get; }

        public User Node => this.CreateProperty(x => x.Node, Octoqit.User.Create);

        /// <summary>
        /// Identifies when the item was starred.
        /// </summary>
        public string StarredAt { get; }

        internal static StargazerEdge Create(IQueryProvider provider, Expression expression)
        {
            return new StargazerEdge(provider, expression);
        }
    }
}