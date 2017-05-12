namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An account on GitHub, with one or more owners, that has repositories, members and teams.
    /// </summary>
    public class Organization : QueryEntity
    {
        public Organization(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A URL pointing to the organization's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarURL(int? size = null) => null;

        /// <summary>
        /// Find gist by repo name.
        /// </summary>
        /// <param name="name">The gist name to find.</param>
        public Gist Gist(string name) => this.CreateMethodCall(x => x.Gist(name), Octokit.GraphQL.Gist.Create);

        /// <summary>
        /// A list of the Gists the user has created.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="visibility">Allows filtering by gist visibility.</param>
        public GistConnection Gists(int? first = null, string after = null, int? last = null, string before = null, GistVisibility? visibility = null) => this.CreateMethodCall(x => x.Gists(first, after, last, before, visibility), Octokit.GraphQL.GistConnection.Create);

        public string Id { get; }

        /// <summary>
        /// The username used to login.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// A list of users who are members of this organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public UserConnection Members(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Members(first, after, last, before), Octokit.GraphQL.UserConnection.Create);

        /// <summary>
        /// The organization's public profile name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The HTTP path for this user
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Find project by number.
        /// </summary>
        /// <param name="number">The project number to find.</param>
        public Project Project(int number) => this.CreateMethodCall(x => x.Project(number), Octokit.GraphQL.Project.Create);

        /// <summary>
        /// A list of projects under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="orderBy">Ordering options for projects returned from the connection</param>
        /// <param name="search">Query to search projects by, currently only searching by name.</param>
        /// <param name="states">A list of states to filter the projects by.</param>
        public ProjectConnection Projects(int? first = null, string after = null, int? last = null, string before = null, ProjectOrder orderBy = null, string search = null, IQueryable<ProjectState> states = null) => this.CreateMethodCall(x => x.Projects(first, after, last, before, orderBy, search, states), Octokit.GraphQL.ProjectConnection.Create);

        /// <summary>
        /// The HTTP path listing organization's projects
        /// </summary>
        public string ProjectsPath { get; }

        /// <summary>
        /// The HTTP url listing organization's projects
        /// </summary>
        public string ProjectsUrl { get; }

        /// <summary>
        /// A list of repositories that the user owns.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy</param>
        /// <param name="isFork">If non-null, filters repositories according to whether they are forks of another repository</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="affiliation">Affiliation options for repositories returned from the connection</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        public RepositoryConnection Repositories(int? first = null, string after = null, int? last = null, string before = null, RepositoryPrivacy? privacy = null, bool? isFork = null, RepositoryOrder orderBy = null, IQueryable<RepositoryAffiliation?> affiliation = null, bool? isLocked = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, privacy, isFork, orderBy, affiliation, isLocked), Octokit.GraphQL.RepositoryConnection.Create);

        /// <summary>
        /// Find Repository.
        /// </summary>
        /// <param name="name">Name of Repository to find.</param>
        public Repository Repository(string name) => this.CreateMethodCall(x => x.Repository(name), Octokit.GraphQL.Repository.Create);

        /// <summary>
        /// Find an organization's team by its slug.
        /// </summary>
        /// <param name="slug">The name or slug of the team to find.</param>
        public Team Team(string slug = null) => this.CreateMethodCall(x => x.Team(slug), Octokit.GraphQL.Team.Create);

        /// <summary>
        /// A list of teams in this organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="privacy">If non-null, filters teams according to privacy</param>
        /// <param name="role">If non-null, filters teams according to whether the viewer is an admin or member on team</param>
        /// <param name="query">If non-null, filters teams with query on team name and team slug</param>
        /// <param name="orderBy">Ordering options for teams returned from the connection</param>
        public TeamConnection Teams(int? first = null, string after = null, int? last = null, string before = null, TeamPrivacy? privacy = null, TeamRole? role = null, string query = null, TeamOrder orderBy = null) => this.CreateMethodCall(x => x.Teams(first, after, last, before, privacy, role, query, orderBy), Octokit.GraphQL.TeamConnection.Create);

        /// <summary>
        /// The HTTP path listing organization's teams
        /// </summary>
        public string TeamsPath { get; }

        /// <summary>
        /// The HTTP url listing organization's teams
        /// </summary>
        public string TeamsUrl { get; }

        /// <summary>
        /// The HTTP url for this user
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Can the current viewer create new projects on this owner.
        /// </summary>
        public bool ViewerCanCreateProjects { get; }

        /// <summary>
        /// Viewer can create repositories on this organization
        /// </summary>
        public bool ViewerCanCreateRepositories { get; }

        internal static Organization Create(IQueryProvider provider, Expression expression)
        {
            return new Organization(provider, expression);
        }
    }
}