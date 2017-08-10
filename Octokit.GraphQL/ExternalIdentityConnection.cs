namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for ExternalIdentity.
    /// </summary>
    public class ExternalIdentityConnection : QueryEntity
    {
        public ExternalIdentityConnection(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryable<ExternalIdentityEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryable<ExternalIdentity> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        internal static ExternalIdentityConnection Create(IQueryProvider provider, Expression expression)
        {
            return new ExternalIdentityConnection(provider, expression);
        }
    }
}