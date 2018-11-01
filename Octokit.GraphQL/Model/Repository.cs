namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository contains the content for a project.
    /// </summary>
    public class Repository : QueryableValue<Repository>
    {
        public Repository(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of users that can be assigned to issues in this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection AssignableUsers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.AssignableUsers(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// A list of branch protection rules for this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public BranchProtectionRuleConnection BranchProtectionRules(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.BranchProtectionRules(first, after, last, before), Octokit.GraphQL.Model.BranchProtectionRuleConnection.Create);

        /// <summary>
        /// Returns the code of conduct for this repository
        /// </summary>
        public CodeOfConduct CodeOfConduct => this.CreateProperty(x => x.CodeOfConduct, Octokit.GraphQL.Model.CodeOfConduct.Create);

        /// <summary>
        /// A list of collaborators associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="affiliation">Collaborators affiliation level with a repository.</param>
        public RepositoryCollaboratorConnection Collaborators(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<CollaboratorAffiliation>? affiliation = null) => this.CreateMethodCall(x => x.Collaborators(first, after, last, before, affiliation), Octokit.GraphQL.Model.RepositoryCollaboratorConnection.Create);

        /// <summary>
        /// A list of commit comments associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public CommitCommentConnection CommitComments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.CommitComments(first, after, last, before), Octokit.GraphQL.Model.CommitCommentConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// The Ref associated with the repository's default branch.
        /// </summary>
        public Ref DefaultBranchRef => this.CreateProperty(x => x.DefaultBranchRef, Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// A list of deploy keys that are on this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public DeployKeyConnection DeployKeys(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.DeployKeys(first, after, last, before), Octokit.GraphQL.Model.DeployKeyConnection.Create);

        /// <summary>
        /// Deployments associated with the repository
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="environments">Environments to list deployments for</param>
        public DeploymentConnection Deployments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<string>>? environments = null) => this.CreateMethodCall(x => x.Deployments(first, after, last, before, environments), Octokit.GraphQL.Model.DeploymentConnection.Create);

        /// <summary>
        /// The description of the repository.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The description of the repository rendered to HTML.
        /// </summary>
        public string DescriptionHTML { get; }

        /// <summary>
        /// The number of kilobytes this repository occupies on disk.
        /// </summary>
        public int? DiskUsage { get; }

        /// <summary>
        /// Returns how many forks there are of this repository in the whole network.
        /// </summary>
        public int ForkCount { get; }

        /// <summary>
        /// A list of direct forked repositories.
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
        public RepositoryConnection Forks(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<RepositoryAffiliation?>>? affiliations = null, Arg<bool>? isLocked = null, Arg<RepositoryOrder>? orderBy = null, Arg<IEnumerable<RepositoryAffiliation?>>? ownerAffiliations = null, Arg<RepositoryPrivacy>? privacy = null) => this.CreateMethodCall(x => x.Forks(first, after, last, before, affiliations, isLocked, orderBy, ownerAffiliations, privacy), Octokit.GraphQL.Model.RepositoryConnection.Create);

        /// <summary>
        /// Indicates if the repository has issues feature enabled.
        /// </summary>
        public bool HasIssuesEnabled { get; }

        /// <summary>
        /// Indicates if the repository has wiki feature enabled.
        /// </summary>
        public bool HasWikiEnabled { get; }

        /// <summary>
        /// The repository's URL.
        /// </summary>
        public string HomepageUrl { get; }

        public ID Id { get; }

        /// <summary>
        /// Indicates if the repository is unmaintained.
        /// </summary>
        public bool IsArchived { get; }

        /// <summary>
        /// Identifies if the repository is a fork.
        /// </summary>
        public bool IsFork { get; }

        /// <summary>
        /// Indicates if the repository has been locked or not.
        /// </summary>
        public bool IsLocked { get; }

        /// <summary>
        /// Identifies if the repository is a mirror.
        /// </summary>
        public bool IsMirror { get; }

        /// <summary>
        /// Identifies if the repository is private.
        /// </summary>
        public bool IsPrivate { get; }

        /// <summary>
        /// Returns a single issue from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the issue to be returned.</param>
        public Issue Issue(Arg<int> number) => this.CreateMethodCall(x => x.Issue(number), Octokit.GraphQL.Model.Issue.Create);

        /// <summary>
        /// Returns a single issue-like object from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the issue to be returned.</param>
        public IssueOrPullRequest IssueOrPullRequest(Arg<int> number) => this.CreateMethodCall(x => x.IssueOrPullRequest(number), Octokit.GraphQL.Model.IssueOrPullRequest.Create);

        /// <summary>
        /// A list of issues that have been opened in the repository.
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
        /// Returns a single label by name
        /// </summary>
        /// <param name="name">Label name</param>
        public Label Label(Arg<string> name) => this.CreateMethodCall(x => x.Label(name), Octokit.GraphQL.Model.Label.Create);

        /// <summary>
        /// A list of labels associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="query">If provided, searches labels by name and description.</param>
        public LabelConnection Labels(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before, query), Octokit.GraphQL.Model.LabelConnection.Create);

        /// <summary>
        /// A list containing a breakdown of the language composition of the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public LanguageConnection Languages(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<LanguageOrder>? orderBy = null) => this.CreateMethodCall(x => x.Languages(first, after, last, before, orderBy), Octokit.GraphQL.Model.LanguageConnection.Create);

        /// <summary>
        /// The license associated with the repository
        /// </summary>
        public License LicenseInfo => this.CreateProperty(x => x.LicenseInfo, Octokit.GraphQL.Model.License.Create);

        /// <summary>
        /// The reason the repository has been locked.
        /// </summary>
        public RepositoryLockReason? LockReason { get; }

        /// <summary>
        /// A list of Users that can be mentioned in the context of the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection MentionableUsers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.MentionableUsers(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// Whether or not PRs are merged with a merge commit on this repository.
        /// </summary>
        public bool MergeCommitAllowed { get; }

        /// <summary>
        /// Returns a single milestone from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the milestone to be returned.</param>
        public Milestone Milestone(Arg<int> number) => this.CreateMethodCall(x => x.Milestone(number), Octokit.GraphQL.Model.Milestone.Create);

        /// <summary>
        /// A list of milestones associated with the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for milestones.</param>
        /// <param name="states">Filter by the state of the milestones.</param>
        public MilestoneConnection Milestones(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<MilestoneOrder>? orderBy = null, Arg<IEnumerable<MilestoneState>>? states = null) => this.CreateMethodCall(x => x.Milestones(first, after, last, before, orderBy, states), Octokit.GraphQL.Model.MilestoneConnection.Create);

        /// <summary>
        /// The repository's original mirror URL.
        /// </summary>
        public string MirrorUrl { get; }

        /// <summary>
        /// The name of the repository.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The repository's name with owner.
        /// </summary>
        public string NameWithOwner { get; }

        /// <summary>
        /// A Git object in the repository
        /// </summary>
        /// <param name="expression">A Git revision expression suitable for rev-parse</param>
        /// <param name="oid">The Git object ID</param>
        public IGitObject Object(Arg<string>? expression = null, Arg<string>? oid = null) => this.CreateMethodCall(x => x.Object(expression, oid), Octokit.GraphQL.Model.Internal.StubIGitObject.Create);

        /// <summary>
        /// The User owner of the repository.
        /// </summary>
        public IRepositoryOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.Internal.StubIRepositoryOwner.Create);

        /// <summary>
        /// The repository parent, if this is a fork.
        /// </summary>
        public Repository Parent => this.CreateProperty(x => x.Parent, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The primary language of the repository's code.
        /// </summary>
        public Language PrimaryLanguage => this.CreateProperty(x => x.PrimaryLanguage, Octokit.GraphQL.Model.Language.Create);

        /// <summary>
        /// Find project by number.
        /// </summary>
        /// <param name="number">The project number to find.</param>
        public Project Project(Arg<int> number) => this.CreateMethodCall(x => x.Project(number), Octokit.GraphQL.Model.Project.Create);

        /// <summary>
        /// A list of projects under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for projects returned from the connection</param>
        /// <param name="search">Query to search projects by, currently only searching by name.</param>
        /// <param name="states">A list of states to filter the projects by.</param>
        public ProjectConnection Projects(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectOrder>? orderBy = null, Arg<string>? search = null, Arg<IEnumerable<ProjectState>>? states = null) => this.CreateMethodCall(x => x.Projects(first, after, last, before, orderBy, search, states), Octokit.GraphQL.Model.ProjectConnection.Create);

        /// <summary>
        /// The HTTP path listing the repository's projects
        /// </summary>
        public string ProjectsResourcePath { get; }

        /// <summary>
        /// The HTTP URL listing the repository's projects
        /// </summary>
        public string ProjectsUrl { get; }

        /// <summary>
        /// A list of protected branches that are on this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProtectedBranchConnection ProtectedBranches(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.ProtectedBranches(first, after, last, before), Octokit.GraphQL.Model.ProtectedBranchConnection.Create);

        /// <summary>
        /// Returns a single pull request from the current repository by number.
        /// </summary>
        /// <param name="number">The number for the pull request to be returned.</param>
        public PullRequest PullRequest(Arg<int> number) => this.CreateMethodCall(x => x.PullRequest(number), Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// A list of pull requests that have been opened in the repository.
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
        /// Identifies when the repository was last pushed to.
        /// </summary>
        public DateTimeOffset? PushedAt { get; }

        /// <summary>
        /// Whether or not rebase-merging is enabled on this repository.
        /// </summary>
        public bool RebaseMergeAllowed { get; }

        /// <summary>
        /// Fetch a given ref from the repository
        /// </summary>
        /// <param name="qualifiedName">The ref to retrieve. Fully qualified matches are checked in order (`refs/heads/master`) before falling back onto checks for short name matches (`master`).</param>
        public Ref Ref(Arg<string> qualifiedName) => this.CreateMethodCall(x => x.Ref(qualifiedName), Octokit.GraphQL.Model.Ref.Create);

        /// <summary>
        /// Fetch a list of refs from the repository
        /// </summary>
        /// <param name="refPrefix">A ref name prefix like `refs/heads/`, `refs/tags/`, etc.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="direction">DEPRECATED: use orderBy. The ordering direction.</param>
        /// <param name="orderBy">Ordering options for refs returned from the connection.</param>
        public RefConnection Refs(Arg<string> refPrefix, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<OrderDirection>? direction = null, Arg<RefOrder>? orderBy = null) => this.CreateMethodCall(x => x.Refs(refPrefix, first, after, last, before, direction, orderBy), Octokit.GraphQL.Model.RefConnection.Create);

        /// <summary>
        /// Lookup a single release given various criteria.
        /// </summary>
        /// <param name="tagName">The name of the Tag the Release was created from</param>
        public Release Release(Arg<string> tagName) => this.CreateMethodCall(x => x.Release(tagName), Octokit.GraphQL.Model.Release.Create);

        /// <summary>
        /// List of releases which are dependent on this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public ReleaseConnection Releases(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ReleaseOrder>? orderBy = null) => this.CreateMethodCall(x => x.Releases(first, after, last, before, orderBy), Octokit.GraphQL.Model.ReleaseConnection.Create);

        /// <summary>
        /// A list of applied repository-topic associations for this repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public RepositoryTopicConnection RepositoryTopics(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.RepositoryTopics(first, after, last, before), Octokit.GraphQL.Model.RepositoryTopicConnection.Create);

        /// <summary>
        /// The HTTP path for this repository
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// A description of the repository, rendered to HTML without any links in it.
        /// </summary>
        /// <param name="limit">How many characters to return.</param>
        public string ShortDescriptionHTML(Arg<int>? limit = null) => null;

        /// <summary>
        /// Whether or not squash-merging is enabled on this repository.
        /// </summary>
        public bool SquashMergeAllowed { get; }

        /// <summary>
        /// The SSH URL to clone this repository
        /// </summary>
        public string SshUrl { get; }

        /// <summary>
        /// A list of users who have starred this starrable.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Order for connection</param>
        public StargazerConnection Stargazers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<StarOrder>? orderBy = null) => this.CreateMethodCall(x => x.Stargazers(first, after, last, before, orderBy), Octokit.GraphQL.Model.StargazerConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this repository
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Indicates whether the viewer has admin permissions on this repository.
        /// </summary>
        public bool ViewerCanAdminister { get; }

        /// <summary>
        /// Can the current viewer create new projects on this owner.
        /// </summary>
        public bool ViewerCanCreateProjects { get; }

        /// <summary>
        /// Check if the viewer is able to change their subscription status for the repository.
        /// </summary>
        public bool ViewerCanSubscribe { get; }

        /// <summary>
        /// Indicates whether the viewer can update the topics of this repository.
        /// </summary>
        public bool ViewerCanUpdateTopics { get; }

        /// <summary>
        /// Returns a boolean indicating whether the viewing user has starred this starrable.
        /// </summary>
        public bool ViewerHasStarred { get; }

        /// <summary>
        /// The users permission level on the repository. Will return null if authenticated as an GitHub App.
        /// </summary>
        public RepositoryPermission? ViewerPermission { get; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        public SubscriptionState? ViewerSubscription { get; }

        /// <summary>
        /// A list of users watching the repository.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Watchers(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Watchers(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        internal static Repository Create(Expression expression)
        {
            return new Repository(expression);
        }
    }
}