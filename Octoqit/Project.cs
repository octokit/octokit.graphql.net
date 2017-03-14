namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Projects manage issues, pull requests and notes within a project owner.
    /// </summary>
    public class Project : QueryEntity
    {
        public Project(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The project's description body.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The projects description body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// Identifities the date and time when the project was closed.
        /// </summary>
        public string ClosedAt { get; }

        /// <summary>
        /// List of columns in the project
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public ProjectColumnConnection Columns(int? first, string after, int? last, string before) => this.CreateMethodCall(x => x.Columns(first, after, last, before), Octoqit.ProjectColumnConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// The user that originally created the project.
        /// </summary>
        public User Creator => this.CreateProperty(x => x.Creator, Octoqit.User.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        public string Id { get; }

        /// <summary>
        /// The project's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The project's number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The project's owner. Currently limited to repositories and organizations.
        /// </summary>
        public IProjectOwner Owner => this.CreateProperty(x => x.Owner, Octoqit.Internal.StubIProjectOwner.Create);

        /// <summary>
        /// The HTTP path for this project
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Whether the project is open or closed.
        /// </summary>
        public ProjectState State { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public string UpdatedAt { get; }

        /// <summary>
        /// The HTTP url for this project
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Can the current viewer edit this project.
        /// </summary>
        public bool ViewerCanEdit { get; }

        internal static Project Create(IQueryProvider provider, Expression expression)
        {
            return new Project(provider, expression);
        }
    }
}