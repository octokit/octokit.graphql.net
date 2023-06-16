namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A workflow inside a project.
    /// </summary>
    public class ProjectV2Workflow : QueryableValue<ProjectV2Workflow>
    {
        internal ProjectV2Workflow(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// The workflows' enabled state.
        /// </summary>
        public bool Enabled { get; }

        public ID Id { get; }

        /// <summary>
        /// The workflows' name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The workflows' number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The project that contains this workflow.
        /// </summary>
        public ProjectV2 Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectV2.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static ProjectV2Workflow Create(Expression expression)
        {
            return new ProjectV2Workflow(expression);
        }
    }
}