namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A team of users in an organization.
    /// </summary>
    public class Team : QueryableValue<Team>
    {
        public Team(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of teams that are ancestors of this team.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public TeamConnection Ancestors(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Ancestors(first, after, last, before), Octokit.GraphQL.Model.TeamConnection.Create);

        /// <summary>
        /// A URL pointing to the team's avatar.
        /// </summary>
        /// <param name="size">The size in pixels of the resulting square image.</param>
        public string AvatarUrl(Arg<int>? size = null) => null;

        /// <summary>
        /// List of child teams belonging to this team
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="immediateOnly">Whether to list immediate child teams or all descendant child teams.</param>
        /// <param name="orderBy">Order for connection</param>
        /// <param name="userLogins">User logins to filter by</param>
        public TeamConnection ChildTeams(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<bool>? immediateOnly = null, Arg<TeamOrder>? orderBy = null, Arg<IEnumerable<string>>? userLogins = null) => this.CreateMethodCall(x => x.ChildTeams(first, after, last, before, immediateOnly, orderBy, userLogins), Octokit.GraphQL.Model.TeamConnection.Create);

        /// <summary>
        /// The slug corresponding to the organization and team.
        /// </summary>
        public string CombinedSlug { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

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

        public ID Id { get; }

        /// <summary>
        /// A list of pending invitations for users to this team
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public OrganizationInvitationConnection Invitations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Invitations(first, after, last, before), Octokit.GraphQL.Model.OrganizationInvitationConnection.Create);

        /// <summary>
        /// A list of users who are members of this team.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="membership">Filter by membership type</param>
        /// <param name="orderBy">Order for the connection.</param>
        /// <param name="query">The search string to look for.</param>
        /// <param name="role">Filter by team member role</param>
        public TeamMemberConnection Members(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<TeamMembershipType>? membership = null, Arg<TeamMemberOrder>? orderBy = null, Arg<string>? query = null, Arg<TeamMemberRole>? role = null) => this.CreateMethodCall(x => x.Members(first, after, last, before, membership, orderBy, query, role), Octokit.GraphQL.Model.TeamMemberConnection.Create);

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
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for the connection.</param>
        /// <param name="query">The search string to look for.</param>
        public TeamRepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<TeamRepositoryOrder>? orderBy = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, orderBy, query), Octokit.GraphQL.Model.TeamRepositoryConnection.Create);

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
        public DateTimeOffset UpdatedAt { get; }

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
        public SubscriptionState? ViewerSubscription { get; }

        internal static Team Create(Expression expression)
        {
            return new Team(expression);
        }
    }
}