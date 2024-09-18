namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'added_to_project' event on a given issue or pull request.
    /// </summary>
    public class AddedToProjectEvent : QueryableValue<AddedToProjectEvent>
    {
        internal AddedToProjectEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The Node ID of the AddedToProjectEvent object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Project referenced by event.
        /// </summary>
        public Project Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.Project.Create);

        /// <summary>
        /// Project card referenced by this project event.
        /// </summary>
        public ProjectCard ProjectCard => this.CreateProperty(x => x.ProjectCard, Octokit.GraphQL.Model.ProjectCard.Create);

        /// <summary>
        /// Column name referenced by this project event.
        /// </summary>
        public string ProjectColumnName { get; }

        internal static AddedToProjectEvent Create(Expression expression)
        {
            return new AddedToProjectEvent(expression);
        }
    }
}