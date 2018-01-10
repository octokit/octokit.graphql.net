namespace Octokit.GraphQL.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A column inside a project.
    /// </summary>
    public class ProjectColumn : QueryEntity
    {
        public ProjectColumn(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// List of cards in the column
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public ProjectCardConnection Cards(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Cards(first, after, last, before), Octokit.GraphQL.Model.ProjectCardConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        public string Id { get; }

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
        public string UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this project column
        /// </summary>
        public string Url { get; }

        internal static ProjectColumn Create(IQueryProvider provider, Expression expression)
        {
            return new ProjectColumn(provider, expression);
        }
    }
}