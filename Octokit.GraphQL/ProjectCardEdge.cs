namespace Octokit.GraphQL
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
    public class ProjectCardEdge : QueryEntity
    {
        public ProjectCardEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ProjectCard Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.ProjectCard.Create);

        internal static ProjectCardEdge Create(IQueryProvider provider, Expression expression)
        {
            return new ProjectCardEdge(provider, expression);
        }
    }
}