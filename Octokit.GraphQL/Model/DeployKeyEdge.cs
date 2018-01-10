namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class DeployKeyEdge : QueryEntity
    {
        public DeployKeyEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public DeployKey Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.DeployKey.Create);

        internal static DeployKeyEdge Create(IQueryProvider provider, Expression expression)
        {
            return new DeployKeyEdge(provider, expression);
        }
    }
}