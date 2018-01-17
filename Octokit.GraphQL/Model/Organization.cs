namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
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
        public string AvatarUrl(int? size = null) => null;

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        [Obsolete(@"Exposed database IDs will eventually be removed in favor of global Relay IDs.")]
        public int? DatabaseId { get; }

        /// <summary>
        /// The organization's public profile description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The organization's public email.
        /// </summary>
        public string Email { get; }

        public string Id { get; }

        /// <summary>
        /// The organization's public profile location.
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// The organization's login name.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// A list of users who are members of this organization.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public UserConnection Members(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Members(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// The organization's public profile name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The HTTP path creating a new team
        /// </summary>
        public string NewTeamResourcePath { get; }

        /// <summary>
        /// The HTTP URL creating a new team
        /// </summary>
        public string NewTeamUrl { get; }

        /// <summary>
        /// The billing email for the organization.
        /// </summary>
        public string OrganizationBillingEmail { get; }

        /// <summary>
        /// A list of repositories this user has pinned to their profile
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="affiliations">Affiliation options for repositories returned from the connection</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        public RepositoryConnection PinnedRepositories(int? first = null, string after = null, int? last = null, string before = null, RepositoryPrivacy? privacy = null, RepositoryOrder orderBy = null, IEnumerable<RepositoryAffiliation?> affiliations = null, bool? isLocked = null) => this.CreateMethodCall(x => x.PinnedRepositories(first, after, last, before, privacy, orderBy, affiliations, isLocked), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// Find project by number.
        /// </summary>
        /// <param name="number">The project number to find.</param>
        public Project Project(int number) => this.CreateMethodCall(x => x.Project(number), Octokit.GraphQL.Model.Project.Create);

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
        public ProjectConnection Projects(int? first = null, string after = null, int? last = null, string before = null, ProjectOrder orderBy = null, string search = null, IEnumerable<ProjectState> states = null) => this.CreateMethodCall(x => x.Projects(first, after, last, before, orderBy, search, states), Octokit.GraphQL.Model.ProjectConnection.Create);

        /// <summary>
        /// The HTTP path listing organization's projects
        /// </summary>
        public string ProjectsResourcePath { get; }

        /// <summary>
        /// The HTTP URL listing organization's projects
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
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="affiliations">Affiliation options for repositories returned from the connection</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="isFork">If non-null, filters repositories according to whether they are forks of another repository</param>
        public RepositoryConnection Repositories(int? first = null, string after = null, int? last = null, string before = null, RepositoryPrivacy? privacy = null, RepositoryOrder orderBy = null, IEnumerable<RepositoryAffiliation?> affiliations = null, bool? isLocked = null, bool? isFork = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, privacy, orderBy, affiliations, isLocked, isFork), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// Find Repository.
        /// </summary>
        /// <param name="name">Name of Repository to find.</param>
        public Repository Repository(string name) => this.CreateMethodCall(x => x.Repository(name), Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this user
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The Organization's SAML Identity Providers
        /// </summary>
        public OrganizationIdentityProvider SamlIdentityProvider => this.CreateProperty(x => x.SamlIdentityProvider, Octokit.GraphQL.Model.OrganizationIdentityProvider.Create);

        /// <summary>
        /// Find an organization's team by its slug.
        /// </summary>
        /// <param name="slug">The name or slug of the team to find.</param>
        public Team Team(string slug) => this.CreateMethodCall(x => x.Team(slug), Octokit.GraphQL.Model.Team.Create);

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
        /// <param name="userLogins">User logins to filter by</param>
        /// <param name="orderBy">Ordering options for teams returned from the connection</param>
        /// <param name="ldapMapped">If true, filters teams that are mapped to an LDAP Group (Enterprise only)</param>
        /// <param name="rootTeamsOnly">If true, restrict to only root teams</param>
        public TeamConnection Teams(int? first = null, string after = null, int? last = null, string before = null, TeamPrivacy? privacy = null, TeamRole? role = null, string query = null, IEnumerable<string> userLogins = null, TeamOrder orderBy = null, bool? ldapMapped = null, bool? rootTeamsOnly = false) => this.CreateMethodCall(x => x.Teams(first, after, last, before, privacy, role, query, userLogins, orderBy, ldapMapped, rootTeamsOnly), Octokit.GraphQL.Model.TeamConnection.Create);

        /// <summary>
        /// The HTTP path listing organization's teams
        /// </summary>
        public string TeamsResourcePath { get; }

        /// <summary>
        /// The HTTP URL listing organization's teams
        /// </summary>
        public string TeamsUrl { get; }

        /// <summary>
        /// The HTTP URL for this user
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Organization is adminable by the viewer.
        /// </summary>
        public bool ViewerCanAdminister { get; }

        /// <summary>
        /// Can the current viewer create new projects on this owner.
        /// </summary>
        public bool ViewerCanCreateProjects { get; }

        /// <summary>
        /// Viewer can create repositories on this organization
        /// </summary>
        public bool ViewerCanCreateRepositories { get; }

        /// <summary>
        /// Viewer can create teams on this organization.
        /// </summary>
        public bool ViewerCanCreateTeams { get; }

        /// <summary>
        /// Viewer is an active member of this organization.
        /// </summary>
        public bool ViewerIsAMember { get; }

        /// <summary>
        /// The organization's public profile URL.
        /// </summary>
        public string WebsiteUrl { get; }

        internal static Organization Create(IQueryProvider provider, Expression expression)
        {
            return new Organization(provider, expression);
        }
    }
}