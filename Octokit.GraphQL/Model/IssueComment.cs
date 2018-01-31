namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a comment on an Issue.
    /// </summary>
    public class IssueComment : QueryableValue<IssueComment>
    {
        public IssueComment(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        public IActor Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Author's association with the subject of the comment.
        /// </summary>
        public CommentAuthorAssociation AuthorAssociation { get; }

        /// <summary>
        /// Identifies the comment body.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The comment body rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// Identifies the body of the issue rendered to text.
        /// </summary>
        public string BodyText { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; }

        /// <summary>
        /// Check if this comment was created via an email reply.
        /// </summary>
        public bool CreatedViaEmail { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        [Obsolete(@"Exposed database IDs will eventually be removed in favor of global Relay IDs.")]
        public int? DatabaseId { get; }

        /// <summary>
        /// The actor who edited the comment.
        /// </summary>
        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        public string Id { get; }

        /// <summary>
        /// Identifies the issue associated with the comment.
        /// </summary>
        public Issue Issue => this.CreateProperty(x => x.Issue, Octokit.GraphQL.Model.Issue.Create);

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public DateTimeOffset? LastEditedAt { get; }

        /// <summary>
        /// Identifies when the comment was published at.
        /// </summary>
        public DateTimeOffset? PublishedAt { get; }

        /// <summary>
        /// Returns the pull request associated with the comment, if this comment was made on a
        /// pull request.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// A list of reactions grouped by content left on the subject.
        /// </summary>
        public IQueryableList<ReactionGroup> ReactionGroups => this.CreateProperty(x => x.ReactionGroups);

        /// <summary>
        /// A list of Reactions left on the Issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="content">Allows filtering Reactions by emoji.</param>
        /// <param name="orderBy">Allows specifying the order in which reactions are returned.</param>
        public ReactionConnection Reactions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ReactionContent>? content = null, Arg<ReactionOrder>? orderBy = null) => this.CreateMethodCall(x => x.Reactions(first, after, last, before, content, orderBy), Octokit.GraphQL.Model.ReactionConnection.Create);

        /// <summary>
        /// The repository associated with this node.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The HTTP path for this issue comment
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        [Obsolete(@"General type updated timestamps will eventually be replaced by other field specific timestamps.")]
        public DateTimeOffset? UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this issue comment
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// A list of edits to this content.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public UserContentEditConnection UserContentEdits(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.UserContentEdits(first, after, last, before), Octokit.GraphQL.Model.UserContentEditConnection.Create);

        /// <summary>
        /// Check if the current viewer can delete this object.
        /// </summary>
        public bool ViewerCanDelete { get; }

        /// <summary>
        /// Can user react to this subject
        /// </summary>
        public bool ViewerCanReact { get; }

        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        public bool ViewerCanUpdate { get; }

        /// <summary>
        /// Reasons why the current viewer can not update this comment.
        /// </summary>
        public IEnumerable<CommentCannotUpdateReason> ViewerCannotUpdateReasons { get; }

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        public bool ViewerDidAuthor { get; }

        internal static IssueComment Create(Expression expression)
        {
            return new IssueComment(expression);
        }
    }
}