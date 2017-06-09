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
    public class RepositoryTopicEdge : QueryEntity
    {
        public RepositoryTopicEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public RepositoryTopic Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.RepositoryTopic.Create);

        internal static RepositoryTopicEdge Create(IQueryProvider provider, Expression expression)
        {
            return new RepositoryTopicEdge(provider, expression);
        }
    }
}