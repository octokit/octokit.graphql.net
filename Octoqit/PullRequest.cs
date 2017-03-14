namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// A repository pull request.
    /// </summary>
    public class PullRequest : QueryEntity
    {
        public PullRequest(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A list of Users assigned to the Issue or Pull Request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public UserConnection Assignees(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Assignees(first, after, last, before), Octoqit.UserConnection.Create);

        /// <summary>
        /// The author of the issue or pull request.
        /// </summary>
        public IAuthor Author => this.CreateProperty(x => x.Author, Octoqit.Internal.StubIAuthor.Create);

        /// <summary>
        /// Identifies the base Ref associated with the pull request.
        /// </summary>
        public Ref BaseRef => this.CreateProperty(x => x.BaseRef, Octoqit.Ref.Create);

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
        /// A list of comments associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public IssueCommentConnection Comments(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before), Octoqit.IssueCommentConnection.Create);

        /// <summary>
        /// A list of commits present in this pull request's head branch not present in the base branch.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public CommitConnection Commits(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Commits(first, after, last, before), Octoqit.CommitConnection.Create);

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
        /// The editor of this pull request's body.
        /// </summary>
        public IAuthor Editor => this.CreateProperty(x => x.Editor, Octoqit.Internal.StubIAuthor.Create);

        /// <summary>
        /// Identifies the head Ref associated with the pull request.
        /// </summary>
        public Ref HeadRef => this.CreateProperty(x => x.HeadRef, Octoqit.Ref.Create);

        /// <summary>
        /// Identifies the name of the head Ref associated with the pull request, even if the ref has been deleted.
        /// </summary>
        public string HeadRefName { get; }

        /// <summary>
        /// The repository associated with this pull request's head Ref.
        /// </summary>
        public Repository HeadRepository => this.CreateProperty(x => x.HeadRepository, Octoqit.Repository.Create);

        /// <summary>
        /// The owner of the repository associated with this pull request's head Ref.
        /// </summary>
        public IRepositoryOwner HeadRepositoryOwner => this.CreateProperty(x => x.HeadRepositoryOwner, Octoqit.Internal.StubIRepositoryOwner.Create);

        public string Id { get; }

        /// <summary>
        /// A list of labels associated with the Issue or Pull Request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public LabelConnection Labels(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before), Octoqit.LabelConnection.Create);

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public string LastEditedAt { get; }

        /// <summary>
        /// Are reaction live updates enabled for this subject.
        /// </summary>
        public bool LiveReactionUpdatesEnabled { get; }

        /// <summary>
        /// The commit that was created when this pull request was merged.
        /// </summary>
        public Commit MergeCommit => this.CreateProperty(x => x.MergeCommit, Octoqit.Commit.Create);

        /// <summary>
        /// Identifies the pull request number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The HTTP path for this issue
        /// </summary>
        public string Path { get; }

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
        public ReactionConnection Reactions(int? first = null, string after = null, int? last = null, string before = null, ReactionContent? content = null, ReactionOrder orderBy = null) => this.CreateMethodCall(x => x.Reactions(first, after, last, before, content, orderBy), Octoqit.ReactionConnection.Create);

        /// <summary>
        /// The websocket channel ID for reaction live updates.
        /// </summary>
        public string ReactionsWebsocket { get; }

        /// <summary>
        /// The repository associated with this pull request.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        /// <summary>
        /// A list of review requests associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public ReviewRequestConnection ReviewRequests(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.ReviewRequests(first, after, last, before), Octoqit.ReviewRequestConnection.Create);

        /// <summary>
        /// A list of reviews associated with the pull request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="states">A list of states to filter the reviews.</param>
        public PullRequestReviewConnection Reviews(int? first = null, string after = null, int? last = null, string before = null, IQueryable<PullRequestReviewState> states = null) => this.CreateMethodCall(x => x.Reviews(first, after, last, before, states), Octoqit.PullRequestReviewConnection.Create);

        /// <summary>
        /// Identifies the state of the pull request.
        /// </summary>
        public PullRequestState State { get; }

        /// <summary>
        /// A list of events associated with an Issue or PullRequest.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="since">Allows filtering timeline events by a `since` timestamp.</param>
        public IssueTimelineConnection Timeline(int? first = null, string after = null, int? last = null, string before = null, string since = null) => this.CreateMethodCall(x => x.Timeline(first, after, last, before, since), Octoqit.IssueTimelineConnection.Create);

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
        /// Check if the current viewer can delete this issue.
        /// </summary>
        public bool ViewerCanDelete { get; }

        /// <summary>
        /// Check if the current viewer edit this comment.
        /// </summary>
        public bool ViewerCanEdit { get; }

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        public bool ViewerCanReact { get; }

        /// <summary>
        /// Errors why the current viewer can not edit this comment.
        /// </summary>
        public IQueryable<CommentCannotEditReason> ViewerCannotEditReasons => this.CreateProperty(x => x.ViewerCannotEditReasons);

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        public bool ViewerDidAuthor { get; }

        /// <summary>
        /// The websocket channel ID for live updates.
        /// </summary>
        /// <param name="channel">The channel to use.</param>
        public IQueryable<string> Websocket(PullRequestPubSubTopic channel) => this.CreateMethodCall(x => x.Websocket(channel));

        internal static PullRequest Create(IQueryProvider provider, Expression expression)
        {
            return new PullRequest(provider, expression);
        }
    }
}