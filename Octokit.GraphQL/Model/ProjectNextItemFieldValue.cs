namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An value of a field in an item of a new Project.
    /// </summary>
    public class ProjectNextItemFieldValue : QueryableValue<ProjectNextItemFieldValue>
    {
        internal ProjectNextItemFieldValue(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The actor who created the item.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        public ID Id { get; }

        /// <summary>
        /// The project field that contains this value.
        /// </summary>
        public ProjectNextField ProjectField => this.CreateProperty(x => x.ProjectField, Octokit.GraphQL.Model.ProjectNextField.Create);

        /// <summary>
        /// The project item that contains this value.
        /// </summary>
        public ProjectNextItem ProjectItem => this.CreateProperty(x => x.ProjectItem, Octokit.GraphQL.Model.ProjectNextItem.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The value of a field
        /// </summary>
        public string Value { get; }

        internal static ProjectNextItemFieldValue Create(Expression expression)
        {
            return new ProjectNextItemFieldValue(expression);
        }
    }
}