namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A single select field inside a project.
    /// </summary>
    public class ProjectV2SingleSelectField : QueryableValue<ProjectV2SingleSelectField>
    {
        internal ProjectV2SingleSelectField(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The field's type.
        /// </summary>
        public ProjectV2FieldType DataType { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        public ID Id { get; }

        /// <summary>
        /// The project field's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Options for the single select field
        /// </summary>
        public IQueryableList<ProjectV2SingleSelectFieldOption> Options => this.CreateProperty(x => x.Options);

        /// <summary>
        /// The project that contains this field.
        /// </summary>
        public ProjectV2 Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectV2.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static ProjectV2SingleSelectField Create(Expression expression)
        {
            return new ProjectV2SingleSelectField(expression);
        }
    }
}