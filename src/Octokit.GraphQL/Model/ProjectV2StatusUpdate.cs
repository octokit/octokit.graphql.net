namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A status update within a project.
    /// </summary>
    public class ProjectV2StatusUpdate : QueryableValue<ProjectV2StatusUpdate>
    {
        internal ProjectV2StatusUpdate(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The body of the status update.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The body of the status update rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The actor who created the status update.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        [Obsolete(@"`databaseId` will be removed because it does not support 64-bit signed integer identifiers. Use `fullDatabaseId` instead. Removal on 2025-04-01 UTC.")]
        public long? DatabaseId { get; }

        /// <summary>
        /// Identifies the primary key from the database as a BigInt.
        /// </summary>
        public string FullDatabaseId { get; }

        /// <summary>
        /// The Node ID of the ProjectV2StatusUpdate object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The project that contains this status update.
        /// </summary>
        public ProjectV2 Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectV2.Create);

        /// <summary>
        /// The start date of the status update.
        /// </summary>
        public string StartDate { get; }

        /// <summary>
        /// The status of the status update.
        /// </summary>
        public ProjectV2StatusUpdateStatus? Status { get; }

        /// <summary>
        /// The target date of the status update.
        /// </summary>
        public string TargetDate { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static ProjectV2StatusUpdate Create(Expression expression)
        {
            return new ProjectV2StatusUpdate(expression);
        }
    }
}