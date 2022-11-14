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
    public class ProjectNextItemEdge : QueryableValue<ProjectNextItemEdge>
    {
        internal ProjectNextItemEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public ProjectNextItem Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.ProjectNextItem.Create);

        internal static ProjectNextItemEdge Create(Expression expression)
        {
            return new ProjectNextItemEdge(expression);
        }
    }
}