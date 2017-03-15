namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// A user is an individual's account on GitHub that owns repositories and can make new content.
    /// </summary>
    public class User : QueryEntity
    {
        public User(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A URL pointing to the user's public avatar.
        /// </summary>
        /// <param name="size">The size of the resulting square image.</param>
        public string AvatarURL(int? size = null) => null;

        /// <summary>
        /// The user's public profile bio.
        /// </summary>
        public string Bio { get; }

        /// <summary>
        /// The user's public profile bio as HTML.
        /// </summary>
        public string BioHTML { get; }

        /// <summary>
        /// The user's public profile company.
        /// </summary>
        public string Company { get; }

        /// <summary>
        /// The user's public profile company as HTML.
        /// </summary>
        public string CompanyHTML { get; }

        /// <summary>
        /// A list of repositories that the user recently contributed to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public RepositoryConnection ContributedRepositories(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.ContributedRepositories(first, after, last, before), Octoqit.RepositoryConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// The user's public profile email.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// A list of users the given user is followed by.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public FollowerConnection Followers(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Followers(first, after, last, before), Octoqit.FollowerConnection.Create);

        /// <summary>
        /// A list of users the given user is following.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public FollowingConnection Following(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Following(first, after, last, before), Octoqit.FollowingConnection.Create);

        /// <summary>
        /// Find gist by repo name.
        /// </summary>
        /// <param name="name">The gist name to find.</param>
        public Gist Gist(string name) => this.CreateMethodCall(x => x.Gist(name), Octoqit.Gist.Create);

        /// <summary>
        /// A list of the Gists the user has created.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="visibility">Allows filtering by gist visibility.</param>
        public GistConnection Gists(int? first = null, string after = null, int? last = null, string before = null, GistVisibility? visibility = null) => this.CreateMethodCall(x => x.Gists(first, after, last, before, visibility), Octoqit.GistConnection.Create);

        public string Id { get; }

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
        public Organization Organization(string login) => this.CreateMethodCall(x => x.Organization(login), Octoqit.Organization.Create);

        /// <summary>
        /// A list of organizations the user belongs to.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public OrganizationConnection Organizations(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Organizations(first, after, last, before), Octoqit.OrganizationConnection.Create);

        /// <summary>
        /// The HTTP path for this user
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// A list of repositories this user has pinned to their profile
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public RepositoryConnection PinnedRepositories(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.PinnedRepositories(first, after, last, before), Octoqit.RepositoryConnection.Create);

        /// <summary>
        /// A list of pull requests assocated with this user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="states">A list of states to filter the pull requests by.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="headRefName">The head ref name to filter the pull requests by.</param>
        /// <param name="baseRefName">The base ref name to filter the pull requests by.</param>
        public PullRequestConnection PullRequests(int? first = null, string after = null, int? last = null, string before = null, IQueryable<PullRequestState> states = null, IQueryable<string> labels = null, string headRefName = null, string baseRefName = null) => this.CreateMethodCall(x => x.PullRequests(first, after, last, before, states, labels, headRefName, baseRefName), Octoqit.PullRequestConnection.Create);

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
        public RepositoryConnection Repositories(int? first = null, string after = null, int? last = null, string before = null, RepositoryPrivacy? privacy = null, bool? isFork = null, RepositoryOrder orderBy = null, IQueryable<RepositoryAffiliation?> affiliation = null, bool? isLocked = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, privacy, isFork, orderBy, affiliation, isLocked), Octoqit.RepositoryConnection.Create);

        /// <summary>
        /// Find Repository.
        /// </summary>
        /// <param name="name">Name of Repository to find.</param>
        public Repository Repository(string name) => this.CreateMethodCall(x => x.Repository(name), Octoqit.Repository.Create);

        /// <summary>
        /// Repositories the user has starred.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="ownedByViewer">Filters starred repositories to only return repositories owned by the viewer.</param>
        /// <param name="orderBy">Order for connection</param>
        public StarredRepositoryConnection StarredRepositories(int? first = null, string after = null, int? last = null, string before = null, bool? ownedByViewer = null, StarOrder orderBy = null) => this.CreateMethodCall(x => x.StarredRepositories(first, after, last, before, ownedByViewer, orderBy), Octoqit.StarredRepositoryConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public string UpdatedAt { get; }

        /// <summary>
        /// The HTTP url for this user
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
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public RepositoryConnection Watching(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Watching(first, after, last, before), Octoqit.RepositoryConnection.Create);

        /// <summary>
        /// A URL pointing to the user's public website/blog.
        /// </summary>
        public string WebsiteURL { get; }

        internal static User Create(IQueryProvider provider, Expression expression)
        {
            return new User(provider, expression);
        }
    }
}