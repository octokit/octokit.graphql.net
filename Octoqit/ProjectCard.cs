namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// A card in a project.
    /// </summary>
    public class ProjectCard : QueryEntity
    {
        public ProjectCard(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The card content item
        /// </summary>
        public IQueryable<object> Content => this.CreateProperty(x => x.Content);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// The user who created this card
        /// </summary>
        public User Creator => this.CreateProperty(x => x.Creator, Octoqit.User.Create);

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
        /// The column that contains this card.
        /// </summary>
        public ProjectColumn ProjectColumn => this.CreateProperty(x => x.ProjectColumn, Octoqit.ProjectColumn.Create);

        /// <summary>
        /// The state of ProjectCard
        /// </summary>
        public ProjectCardState? State { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public string UpdatedAt { get; }

        internal static ProjectCard Create(IQueryProvider provider, Expression expression)
        {
            return new ProjectCard(provider, expression);
        }
    }
}