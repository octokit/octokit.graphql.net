namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class RepositoryEdge : QueryEntity
    {
        public RepositoryEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Repository Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Repository.Create);

        internal static RepositoryEdge Create(IQueryProvider provider, Expression expression)
        {
            return new RepositoryEdge(provider, expression);
        }
    }
}