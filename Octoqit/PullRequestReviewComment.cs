namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// A review comment associated with a given repository pull request.
    /// </summary>
    public class PullRequestReviewComment : QueryEntity
    {
        public PullRequestReviewComment(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The author of the comment.
        /// </summary>
        public IAuthor Author => this.CreateProperty(x => x.Author, Octoqit.Internal.StubIAuthor.Create);

        /// <summary>
        /// The comment body of this review comment.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The comment body of this review comment rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// The comment body of this review comment rendered as plain text.
        /// </summary>
        public string BodyText { get; }

        /// <summary>
        /// Identifies the commit associated with the comment.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octoqit.Commit.Create);

        /// <summary>
        /// Identifies when the comment was created.
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
        /// The diff hunk to which the comment applies.
        /// </summary>
        public string DiffHunk { get; }

        /// <summary>
        /// The editor of the comment.
        /// </summary>
        public IAuthor Editor => this.CreateProperty(x => x.Editor, Octoqit.Internal.StubIAuthor.Create);

        public string Id { get; }

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public string LastEditedAt { get; }

        /// <summary>
        /// Are reaction live updates enabled for this subject.
        /// </summary>
        public bool LiveReactionUpdatesEnabled { get; }

        /// <summary>
        /// Identifies the original commit associated with the comment.
        /// </summary>
        public Commit OriginalCommit => this.CreateProperty(x => x.OriginalCommit, Octoqit.Commit.Create);

        /// <summary>
        /// The original line index in the diff to which the comment applies.
        /// </summary>
        public int OriginalPosition { get; }

        /// <summary>
        /// The path to which the comment applies.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The line index in the diff to which the comment applies.
        /// </summary>
        public int? Position { get; }

        /// <summary>
        /// The pull request associated with this review comment.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octoqit.PullRequest.Create);

        /// <summary>
        /// The pull request review associated with this review comment.
        /// </summary>
        public PullRequestReview PullRequestReview => this.CreateProperty(x => x.PullRequestReview, Octoqit.PullRequestReview.Create);

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
        public ReactionConnection Reactions(int? first, string after, int? last, string before, ReactionContent? content, ReactionOrder orderBy) => this.CreateMethodCall(x => x.Reactions(first, after, last, before, content, orderBy), Octoqit.ReactionConnection.Create);

        /// <summary>
        /// The websocket channel ID for reaction live updates.
        /// </summary>
        public string ReactionsWebsocket { get; }

        /// <summary>
        /// The repository associated with this review comment.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        /// <summary>
        /// Identifies when the comment was last updated.
        /// </summary>
        public string UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL permalink for this review comment.
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// Check if the current viewer can delete this comment.
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

        internal static PullRequestReviewComment Create(IQueryProvider provider, Expression expression)
        {
            return new PullRequestReviewComment(provider, expression);
        }
    }
}