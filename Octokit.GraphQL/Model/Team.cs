namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A team of users in an organization.
    /// </summary>
    public class Team : QueryableValue<Team>
    {
        public Team(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A list of teams that are ancestors of this team.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public TeamConnection Ancestors(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Ancestors(first, after, last, before), Octokit.GraphQL.Model.TeamConnection.Create);

        /// <summary>
        /// List of child teams belonging to this team
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="orderBy">Order for connection</param>
        /// <param name="userLogins">User logins to filter by</param>
        /// <param name="immediateOnly">Whether to list immediate child teams or all descendant child teams.</param>
        public TeamConnection ChildTeams(int? first = null, string after = null, int? last = null, string before = null, TeamOrder orderBy = null, IEnumerable<string> userLogins = null, bool? immediateOnly = true) => this.CreateMethodCall(x => x.ChildTeams(first, after, last, before, orderBy, userLogins, immediateOnly), Octokit.GraphQL.Model.TeamConnection.Create);

        /// <summary>
        /// The slug corresponding to the organization and team.
        /// </summary>
        public string CombinedSlug { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; }

        /// <summary>
        /// The description of the team.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The HTTP path for editing this team
        /// </summary>
        public string EditTeamResourcePath { get; }

        /// <summary>
        /// The HTTP URL for editing this team
        /// </summary>
        public string EditTeamUrl { get; }

        public string Id { get; }

        /// <summary>
        /// A list of pending invitations for users to this team
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public OrganizationInvitationConnection Invitations(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Invitations(first, after, last, before), Octokit.GraphQL.Model.OrganizationInvitationConnection.Create);

        /// <summary>
        /// A list of users who are members of this team.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="query">The search string to look for.</param>
        /// <param name="membership">Filter by membership type</param>
        /// <param name="role">Filter by team member role</param>
        public TeamMemberConnection Members(int? first = null, string after = null, int? last = null, string before = null, string query = null, TeamMembershipType? membership = TeamMembershipType.All, TeamMemberRole? role = null) => this.CreateMethodCall(x => x.Members(first, after, last, before, query, membership, role), Octokit.GraphQL.Model.TeamMemberConnection.Create);

        /// <summary>
        /// The HTTP path for the team' members
        /// </summary>
        public string MembersResourcePath { get; }

        /// <summary>
        /// The HTTP URL for the team' members
        /// </summary>
        public string MembersUrl { get; }

        /// <summary>
        /// The name of the team.
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
        /// The organization that owns this team.
        /// </summary>
        public Organization Organization => this.CreateProperty(x => x.Organization, Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// The parent team of the team.
        /// </summary>
        public Team ParentTeam => this.CreateProperty(x => x.ParentTeam, Octokit.GraphQL.Model.Team.Create);

        /// <summary>
        /// The level of privacy the team has.
        /// </summary>
        public TeamPrivacy Privacy { get; }

        /// <summary>
        /// A list of repositories this team has access to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="query">The search string to look for.</param>
        /// <param name="orderBy">Order for the connection.</param>
        public TeamRepositoryConnection Repositories(int? first = null, string after = null, int? last = null, string before = null, string query = null, TeamRepositoryOrder orderBy = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, query, orderBy), Octokit.GraphQL.Model.TeamRepositoryConnection.Create);

        /// <summary>
        /// The HTTP path for this team's repositories
        /// </summary>
        public string RepositoriesResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this team's repositories
        /// </summary>
        public string RepositoriesUrl { get; }

        /// <summary>
        /// The HTTP path for this team
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// The slug corresponding to the team.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// The HTTP path for this team's teams
        /// </summary>
        public string TeamsResourcePath { get; }

        /// <summary>
        /// The HTTP URL for this team's teams
        /// </summary>
        public string TeamsUrl { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        [Obsolete(@"General type updated timestamps will eventually be replaced by other field specific timestamps.")]
        public DateTimeOffset? UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this team
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Team is adminable by the viewer.
        /// </summary>
        public bool ViewerCanAdminister { get; }

        /// <summary>
        /// Check if the viewer is able to change their subscription status for the repository.
        /// </summary>
        public bool ViewerCanSubscribe { get; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        public SubscriptionState ViewerSubscription { get; }

        internal static Team Create(IQueryProvider provider, Expression expression)
        {
            return new Team(provider, expression);
        }
    }
}