namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class ProjectViewEdge : QueryableValue<ProjectViewEdge>
    {
        internal ProjectViewEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ProjectView Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.ProjectView.Create);

        internal static ProjectViewEdge Create(Expression expression)
        {
            return new ProjectViewEdge(expression);
        }
    }
}