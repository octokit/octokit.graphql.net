namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class IntegrationEdge : QueryEntity
    {
        public IntegrationEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Integration Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Integration.Create);

        internal static IntegrationEdge Create(IQueryProvider provider, Expression expression)
        {
            return new IntegrationEdge(provider, expression);
        }
    }
}