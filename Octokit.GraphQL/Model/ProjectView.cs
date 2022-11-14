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
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public int? DatabaseId { get; }

        /// <summary>
        /// The project view's filter.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public string Filter { get; }

        /// <summary>
        /// The view's group-by field.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public IEnumerable<int> GroupBy { get; }

        public ID Id { get; }

        /// <summary>
        /// The view's filtered items.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectNextItemConnection Items(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Items(first, after, last, before), Octokit.GraphQL.Model.ProjectNextItemConnection.Create);

        /// <summary>
        /// The project view's layout.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public ProjectViewLayout Layout { get; }

        /// <summary>
        /// The project view's name.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public string Name { get; }

        /// <summary>
        /// The project view's number.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public int Number { get; }

        /// <summary>
        /// The project that contains this view.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public ProjectNext Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectNext.Create);

        /// <summary>
        /// The view's sort-by config.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public IQueryableList<SortBy> SortBy => this.CreateProperty(x => x.SortBy);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The view's vertical-group-by field.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public IEnumerable<int> VerticalGroupBy { get; }

        /// <summary>
        /// The view's visible fields.
        /// </summary>
        [Obsolete(@"The `ProjectNext` API is deprecated in favour of the more capable `ProjectV2` API. Follow the ProjectV2 guide at https://github.blog/changelog/2022-06-23-the-new-github-issues-june-23rd-update/, to find a suitable replacement. Removal on 2022-10-01 UTC.")]
        public IEnumerable<int> VisibleFields { get; }

        internal static ProjectView Create(Expression expression)
        {
            return new ProjectView(expression);
        }
    }
}