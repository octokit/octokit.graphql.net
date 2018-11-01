namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A user is an individual's account on GitHub that owns repositories and can make new content.
    /// </summary>
    public class User : QueryableValue<User>
    {
        public User(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A URL pointing to the user's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarUrl(Arg<int>? size = null) => null;

        /// <summary>
        /// The user's public profile bio.
        /// </summary>
        public string Bio { get; }

        /// <summary>
        /// The user's public profile bio as HTML.
        /// </summary>
        public string BioHTML { get; }

        /// <summary>
        /// A list of commit comments made by this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public CommitCommentConnection CommitComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.CommitComments(first, after, last, before), Octokit.GraphQL.Model.CommitCommentConnection.Create);

        /// <summary>
        /// The user's public profile company.
        /// </summary>
        public string Company { get; }

        /// <summary>
        /// The user's public profile company as HTML.
        /// </summary>
        public string CompanyHTML { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// The user's publicly visible profile email.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// A list of users the given user is followed by.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public FollowerConnection Followers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Followers(first, after, last, before), Octokit.GraphQL.Model.FollowerConnection.Create);

        /// <summary>
        /// A list of users the given user is following.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public FollowingConnection Following(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Following(first, after, last, before), Octokit.GraphQL.Model.FollowingConnection.Create);

        /// <summary>
        /// Find gist by repo name.
        /// </summary>
        /// <param name="name">The gist name to find.</param>
        public Gist Gist(Arg<string> name) => this.CreateMethodCall(x => x.Gist(name), Octokit.GraphQL.Model.Gist.Create);

        /// <summary>
        /// A list of gist comments made by this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public GistCommentConnection GistComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.GistComments(first, after, last, before), Octokit.GraphQL.Model.GistCommentConnection.Create);

        /// <summary>
        /// A list of the Gists the user has created.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for gists returned from the connection</param>
        /// <param name="privacy">Filters Gists according to privacy.</param>
        public GistConnection Gists(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<GistOrder>? orderBy = null, Arg<GistPrivacy>? privacy = null) => this.CreateMethodCall(x => x.Gists(first, after, last, before, orderBy, privacy), Octokit.GraphQL.Model.GistConnection.Create);

        public ID Id { get; }

        /// <summary>
        /// Whether or not this user is a participant in the GitHub Security Bug Bounty.
        /// </summary>
        public bool IsBountyHunter { get; }

        /// <summary>
        /// Whether or not this user is a participant in the GitHub Campus Experts Program.
        /// </summary>
        public bool IsCampusExpert { get; }

        /// <summary>
        /// Whether or not this user is a GitHub Developer Program member.
        /// </summary>
        public bool IsDeveloperProgramMember { get; }

        /// <summary>
        /// Whether or not this user is a GitHub employee.
        /// </summary>
        public bool IsEmployee { get; }

        /// <summary>
        /// Whether or not the user has marked themselves as for hire.
        /// </summary>
        public bool IsHireable { get; }

        /// <summary>
        /// Whether or not this user is a site administrator.
        /// </summary>
        public bool IsSiteAdmin { get; }

        /// <summary>
        /// Whether or not this user is the viewing user.
        /// </summary>
        public bool IsViewer { get; }

        /// <summary>
        /// A list of issue comments made by this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public IssueCommentConnection IssueComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.IssueComments(first, after, last, before), Octokit.GraphQL.Model.IssueCommentConnection.Create);

        /// <summary>
        /// A list of issues associated with this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for issues returned from the connection.</param>
        /// <param name="states">A list of states to filter the issues by.</param>
        public IssueConnection Issues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<string>>? labels = null, Arg<IssueOrder>? orderBy = null, Arg<IEnumerable<IssueState>>? states = null) => this.CreateMethodCall(x => x.Issues(first, after, last, before, labels, orderBy, states), Octokit.GraphQL.Model.IssueConnection.Create);

        /// <summary>
        /// The user's public profile location.
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// The username used to login.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// The user's public profile name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Find an organization by its login that the user belongs to.
        /// </summary>
        /// <param name="login">The login of the organization to find.</param>
        public Organization Organization(Arg<string> login) => this.CreateMethodCall(x => x.Organization(login), Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// A list of organizations the user belongs to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public OrganizationConnection Organizations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Organizations(first, after, last, before), Octokit.GraphQL.Model.OrganizationConnection.Create);

        /// <summary>
        /// A list of repositories this user has pinned to their profile
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliations">Array of viewer's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the current viewer owns.</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="ownerAffiliations">Array of owner's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the organization or user being viewed owns.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy</param>
        public RepositoryConnection PinnedRepositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null) => this.CreateMethodCall(x => x.PinnedRepositories(first, after, last, before, affiliations, isLocked, orderBy, ownerAffiliations, privacy), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// A list of public keys associated with this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public PublicKeyConnection PublicKeys(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.PublicKeys(first, after, last, before), Octokit.GraphQL.Model.PublicKeyConnection.Create);

        /// <summary>
        /// A list of pull requests associated with this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="baseRefName">The base ref name to filter the pull requests by.</param>
        /// <param name="headRefName">The head ref name to filter the pull requests by.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for pull requests returned from the connection.</param>
        /// <param name="states">A list of states to filter the pull requests by.</param>
        public PullRequestConnection PullRequests(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? baseRefName = null, Arg<string>? headRefName = null, Arg<IEnumerable<string>>? labels = null, Arg<IssueOrder>? orderBy = null, Arg<IEnumerable<PullRequestState>>? states = null) => this.CreateMethodCall(x => x.PullRequests(first, after, last, before, baseRefName, headRefName, labels, orderBy, states), Octokit.GraphQL.Model.PullRequestConnection.Create);

        /// <summary>
        /// A list of repositories that the user owns.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliations">Array of viewer's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the current viewer owns.</param>
        /// <param name="isFork">If non-null, filters repositories according to whether they are forks of another repository</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="ownerAffiliations">Array of owner's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the organization or user being viewed owns.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy</param>
        public RepositoryConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? isFork = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, affiliations, isFork, isLocked, orderBy, ownerAffiliations, privacy), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// A list of repositories that the user recently contributed to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="contributionTypes">If non-null, include only the specified types of contributions. The GitHub.com UI uses [COMMIT, ISSUE, PULL_REQUEST, REPOSITORY]</param>
        /// <param name="includeUserRepositories">If true, include user repositories</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy</param>
        public RepositoryConnection RepositoriesContributedTo(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryContributionType?>>? contributionTypes = null, Arg<bool>? includeUserRepositories = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<RepositoryPrivacy>? privacy = null) => this.CreateMethodCall(x => x.RepositoriesContributedTo(first, after, last, before, contributionTypes, includeUserRepositories, isLocked, orderBy, privacy), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// Find Repository.
        /// </summary>
        /// <param name="name">Name of Repository to find.</param>
        public Repository Repository(Arg<string> name) => this.CreateMethodCall(x => x.Repository(name), Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this user
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Repositories the user has starred.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        /// <param name="ownedByViewer">Filters starred repositories to only return repositories owned by the viewer.</param>
        public StarredRepositoryConnection StarredRepositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<StarOrder>? orderBy = null, Arg<bool>? ownedByViewer = null) => this.CreateMethodCall(x => x.StarredRepositories(first, after, last, before, orderBy, ownedByViewer), Octokit.GraphQL.Model.StarredRepositoryConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this user
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Whether or not the viewer is able to follow the user.
        /// </summary>
        public bool ViewerCanFollow { get; }

        /// <summary>
        /// Whether or not this user is followed by the viewer.
        /// </summary>
        public bool ViewerIsFollowing { get; }

        /// <summary>
        /// A list of repositories the given user is watching.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliations">Affiliation options for repositories returned from the connection</param>
        /// <param name="isLocked">If non-null, filters repositories according to whether they have been locked</param>
        /// <param name="orderBy">Ordering options for repositories returned from the connection</param>
        /// <param name="ownerAffiliations">Array of owner's affiliation options for repositories returned from the connection. For example, OWNER will include only repositories that the organization or user being viewed owns.</param>
        /// <param name="privacy">If non-null, filters repositories according to privacy</param>
        public RepositoryConnection Watching(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null) => this.CreateMethodCall(x => x.Watching(first, after, last, before, affiliations, isLocked, orderBy, ownerAffiliations, privacy), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// A URL pointing to the user's public website/blog.
        /// </summary>
        public string WebsiteUrl { get; }

        internal static User Create(Expression expression)
        {
            return new User(expression);
        }
    }
}