namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A column inside a project.
    /// </summary>
    public class ProjectColumn : QueryableValue<ProjectColumn>
    {
        public ProjectColumn(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// List of cards in the column
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public ProjectCardConnection Cards(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Cards(first, after, last, before), Octokit.GraphQL.Model.ProjectCardConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        [Obsolete(@"Exposed database IDs will eventually be removed in favor of global Relay IDs. Use `Node.id` instead. Removal on 2018-07-01 UTC.")]
        public int? DatabaseId { get; }

        public ID Id { get; }

        /// <summary>
        /// The project column's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The project that contains this column.
        /// </summary>
        public Project Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.Project.Create);

        /// <summary>
        /// The HTTP path for this project column
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        [Obsolete(@"General type updated timestamps will eventually be replaced by other field specific timestamps. Removal on 2018-07-01 UTC.")]
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this project column
        /// </summary>
        public string Url { get; }

        internal static ProjectColumn Create(Expression expression)
        {
            return new ProjectColumn(expression);
        }
    }
}