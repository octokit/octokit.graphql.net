namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The connection type for Organization.
    /// </summary>
    public class OrganizationConnection : QueryEntity
    {
        public OrganizationConnection(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryable<OrganizationEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryable<Organization> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.PageInfo.Create);

        /// <summary>
        /// Identifies the total count of items in the connection.
        /// </summary>
        public int TotalCount { get; }

        internal static OrganizationConnection Create(IQueryProvider provider, Expression expression)
        {
            return new OrganizationConnection(provider, expression);
        }
    }
}