namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A field inside a project.
    /// </summary>
    public class ProjectNextField : QueryableValue<ProjectNextField>
    {
        internal ProjectNextField(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The field's type.
        /// </summary>
        public ProjectNextFieldType DataType { get; }

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
        /// The project that contains this field.
        /// </summary>
        public ProjectNext Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectNext.Create);

        /// <summary>
        /// The field's settings.
        /// </summary>
        public string Settings { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static ProjectNextField Create(Expression expression)
        {
            return new ProjectNextField(expression);
        }
    }
}