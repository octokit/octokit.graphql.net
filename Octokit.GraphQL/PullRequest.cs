namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository pull request.
    /// </summary>
    public class PullRequest : QueryEntity
    {
        public PullRequest(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A list of Users assigned to this object.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public UserConnection Assignees(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Assignees(first, after, last, before), Octokit.GraphQL.UserConnection.Create);

        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        public IActor Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the base Ref associated with the pull request.
        /// </summary>
        public Ref BaseRef => this.CreateProperty(x => x.BaseRef, Octokit.GraphQL.Ref.Create);

        /// <summary>
        /// Identifies the name of the base Ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string BaseRefName { get; }

        /// <summary>
        /// Identifies the body of the pull request.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// Identifies the body of the pull request rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// Identifies the body of the pull request rendered to text.
        /// </summary>
        public string BodyText { get; }

        /// <summary>
        /// true if the object is `closed` (definition of closed may depend on type)
        /// </summary>
        public bool Closed { get; }

        /// <summary>
        /// A list of comments associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public IssueCommentConnection Comments(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before), Octokit.GraphQL.IssueCommentConnection.Create);

        /// <summary>
        /// A list of commits present in this pull request's head branch not present in the base branch.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public PullRequestCommitConnection Commits(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Commits(first, after, last, before), Octokit.GraphQL.PullRequestCommitConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// Check if this comment was created via an email reply.
        /// </summary>
        public bool CreatedViaEmail { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// The actor who edited this pull request's body.
        /// </summary>
        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the head Ref associated with the pull request.
        /// </summary>
        public Ref HeadRef => this.CreateProperty(x => x.HeadRef, Octokit.GraphQL.Ref.Create);

        /// <summary>
        /// Identifies the name of the head Ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string HeadRefName { get; }

        /// <summary>
        /// The repository associated with this pull request's head Ref.
        /// </summary>
        public Repository HeadRepository => this.CreateProperty(x => x.HeadRepository, Octokit.GraphQL.Repository.Create);

        /// <summary>
        /// The owner of the repository associated with this pull request's head Ref.
        /// </summary>
        public IRepositoryOwner HeadRepositoryOwner => this.CreateProperty(x => x.HeadRepositoryOwner, Octokit.GraphQL.Internal.StubIRepositoryOwner.Create);

        public string Id { get; }

        /// <summary>
        /// A list of labels associated with the object.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public LabelConnection Labels(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before), Octokit.GraphQL.LabelConnection.Create);

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public string LastEditedAt { get; }

        /// <summary>
        /// `true` if the object is locked
        /// </summary>
        public bool Locked { get; }

        /// <summary>
        /// The commit that was created when this pull request was merged.
        /// </summary>
        public Commit MergeCommit => this.CreateProperty(x => x.MergeCommit, Octokit.GraphQL.Commit.Create);

        /// <summary>
        /// Whether or not the pull request can be merged based on the existence of merge conflicts.
        /// </summary>
        public MergeableState Mergeable { get; }

        /// <summary>
        /// Whether or not the pull request was merged.
        /// </summary>
        public bool Merged { get; }

        /// <summary>
        /// The date and time that the pull request was merged.
        /// </summary>
        public string MergedAt { get; }

        /// <summary>
        /// Identifies the pull request number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// A list of Users that are participating in the Pull Request conversation.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public UserConnection Participants(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Participants(first, after, last, before), Octokit.GraphQL.UserConnection.Create);

        /// <summary>
        /// The HTTP path for this issue
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The commit that GitHub automatically generated to test if this pull request could be merged. This field will not return a value if the pull request is merged, or if the test merge commit is still being generated. See the `mergeable` field for more details on the mergeability of the pull request.
        /// </summary>
        public Commit PotentialMergeCommit => this.CreateProperty(x => x.PotentialMergeCommit, Octokit.GraphQL.Commit.Create);

        /// <summary>
        /// Identifies when the comment was published at.
        /// </summary>
        public string PublishedAt { get; }

        /// <summary>
        /// A list of reactions grouped by content left on the subject.
        /// </summary>
        public IQueryable<ReactionGroup> ReactionGroups => this.CreateProperty(x => x.ReactionGroups);

        /// <summary>
        /// A list of Reactions left on the Issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="content">Allows filtering Reactions by emoji.</param>
        /// <param name="orderBy">Allows specifying the order in which reactions are returned.</param>
        public ReactionConnection Reactions(int? first = null, string after = null, int? last = null, string before = null, ReactionContent? content = null, ReactionOrder orderBy = null) => this.CreateMethodCall(x => x.Reactions(first, after, last, before, content, orderBy), Octokit.GraphQL.ReactionConnection.Create);

        /// <summary>
        /// The repository associated with this pull request.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Repository.Create);

        /// <summary>
        /// The HTTP path for this issue
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// A list of review requests associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public ReviewRequestConnection ReviewRequests(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.ReviewRequests(first, after, last, before), Octokit.GraphQL.ReviewRequestConnection.Create);

        /// <summary>
        /// A list of reviews associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="states">A list of states to filter the reviews.</param>
        public PullRequestReviewConnection Reviews(int? first = null, string after = null, int? last = null, string before = null, IQueryable<PullRequestReviewState> states = null) => this.CreateMethodCall(x => x.Reviews(first, after, last, before, states), Octokit.GraphQL.PullRequestReviewConnection.Create);

        /// <summary>
        /// Identifies the state of the pull request.
        /// </summary>
        public PullRequestState State { get; }

        /// <summary>
        /// A list of reviewer suggestions based on commit history and past review comments.
        /// </summary>
        public IQueryable<SuggestedReviewer> SuggestedReviewers => this.CreateProperty(x => x.SuggestedReviewers);

        /// <summary>
        /// A list of events associated with a PullRequest.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="since">Allows filtering timeline events by a `since` timestamp.</param>
        public PullRequestTimelineConnection Timeline(int? first = null, string after = null, int? last = null, string before = null, string since = null) => this.CreateMethodCall(x => x.Timeline(first, after, last, before, since), Octokit.GraphQL.PullRequestTimelineConnection.Create);

        /// <summary>
        /// Identifies the pull request title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public string UpdatedAt { get; }

        /// <summary>
        /// The HTTP url for this issue
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The integration the pull request was authored via.
        /// </summary>
        public Integration ViaIntegration => this.CreateProperty(x => x.ViaIntegration, Octokit.GraphQL.Integration.Create);

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        public bool ViewerCanReact { get; }

        /// <summary>
        /// Check if the viewer is able to change their subscription status for the repository.
        /// </summary>
        public bool ViewerCanSubscribe { get; }

        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        public bool ViewerCanUpdate { get; }

        /// <summary>
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        public IQueryable<CommentCannotUpdateReason> ViewerCannotUpdateReasons => this.CreateProperty(x => x.ViewerCannotUpdateReasons);

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        public bool ViewerDidAuthor { get; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the repository.
        /// </summary>
        public SubscriptionState ViewerSubscription { get; }

        internal static PullRequest Create(IQueryProvider provider, Expression expression)
        {
            return new PullRequest(provider, expression);
        }
    }
}