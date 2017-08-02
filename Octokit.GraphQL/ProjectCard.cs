namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A card in a project.
    /// </summary>
    public class ProjectCard : QueryEntity
    {
        public ProjectCard(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The project column this card is associated under. A card may only belong to one
project column at a time. The column field will be null if the card is created
in a pending state and has yet to be associated with a column. Once cards are
associated with a column, they will not become pending in the future.

        /// </summary>
        public ProjectColumn Column => this.CreateProperty(x => x.Column, Octokit.GraphQL.ProjectColumn.Create);

        /// <summary>
        /// The card content item
        /// </summary>
        public ProjectCardItem Content => this.CreateProperty(x => x.Content, Octokit.GraphQL.ProjectCardItem.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// The actor who created this card
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        public string Id { get; }

        /// <summary>
        /// The card note
        /// </summary>
        public string Note { get; }

        /// <summary>
        /// The project that contains this card.
        /// </summary>
        public Project Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Project.Create);

        /// <summary>
        /// The column that contains this card.
        /// </summary>
        public ProjectColumn ProjectColumn => this.CreateProperty(x => x.ProjectColumn, Octokit.GraphQL.ProjectColumn.Create);

        /// <summary>
        /// The HTTP path for this card
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The state of ProjectCard
        /// </summary>
        public ProjectCardState? State { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public string UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this card
        /// </summary>
        public string Url { get; }

        internal static ProjectCard Create(IQueryProvider provider, Expression expression)
        {
            return new ProjectCard(provider, expression);
        }
    }
}