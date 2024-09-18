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
        [Obsolete(@"`databaseId` will be removed because it does not support 64-bit signed integer identifiers. Use `fullDatabaseId` instead. Removal on 2025-04-01 UTC.")]
        public long? DatabaseId { get; }

        /// <summary>
        /// Whether the workflow is enabled.
        /// </summary>
        public bool Enabled { get; }

        /// <summary>
        /// Identifies the primary key from the database as a BigInt.
        /// </summary>
        public string FullDatabaseId { get; }

        /// <summary>
        /// The Node ID of the ProjectV2Workflow object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The name of the workflow.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The number of the workflow.
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