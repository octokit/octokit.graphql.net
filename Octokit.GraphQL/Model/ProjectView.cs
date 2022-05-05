namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A view within a Project.
    /// </summary>
    public class ProjectView : QueryableValue<ProjectView>
    {
        internal ProjectView(Expression expression) : base(expression)
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
        /// The project view's filter.
        /// </summary>
        public string Filter { get; }

        /// <summary>
        /// The view's group-by field.
        /// </summary>
        public IEnumerable<int> GroupBy { get; }

        public ID Id { get; }

        /// <summary>
        /// The project view's layout.
        /// </summary>
        public ProjectViewLayout Layout { get; }

        /// <summary>
        /// The project view's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The project view's number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The project that contains this view.
        /// </summary>
        public ProjectNext Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectNext.Create);

        /// <summary>
        /// The view's sort-by config.
        /// </summary>
        public IQueryableList<SortBy> SortBy => this.CreateProperty(x => x.SortBy);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The view's vertical-group-by field.
        /// </summary>
        public IEnumerable<int> VerticalGroupBy { get; }

        /// <summary>
        /// The view's visible fields.
        /// </summary>
        public IEnumerable<int> VisibleFields { get; }

        internal static ProjectView Create(Expression expression)
        {
            return new ProjectView(expression);
        }
    }
}