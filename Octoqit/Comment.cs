namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a comment.
    /// </summary>
    public interface IComment : IQueryEntity
    {
        /// <summary>
        /// The author of the comment.
        /// </summary>
        IAuthor Author { get; }

        /// <summary>
        /// The comment body as Markdown.
        /// </summary>
        string Body { get; }

        /// <summary>
        /// The comment body rendered to HTML.
        /// </summary>
        string BodyHTML { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        string CreatedAt { get; }

        /// <summary>
        /// Check if this comment was created via an email reply.
        /// </summary>
        bool CreatedViaEmail { get; }

        /// <summary>
        /// The editor of the comment.
        /// </summary>
        IAuthor Editor { get; }

        string Id { get; }

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        string LastEditedAt { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        string UpdatedAt { get; }

        /// <summary>
        /// Check if the current viewer can delete this comment.
        /// </summary>
        bool ViewerCanDelete { get; }

        /// <summary>
        /// Check if the current viewer edit this comment.
        /// </summary>
        bool ViewerCanEdit { get; }

        /// <summary>
        /// Errors why the current viewer can not edit this comment.
        /// </summary>
        IQueryable<CommentCannotEditReason> ViewerCannotEditReasons { get; }

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        bool ViewerDidAuthor { get; }
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubIComment : QueryEntity, IComment
    {
        public StubIComment(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public IAuthor Author => this.CreateProperty(x => x.Author, Octoqit.Internal.StubIAuthor.Create);

        public string Body { get; }

        public string BodyHTML { get; }

        public string CreatedAt { get; }

        public bool CreatedViaEmail { get; }

        public IAuthor Editor => this.CreateProperty(x => x.Editor, Octoqit.Internal.StubIAuthor.Create);

        public string Id { get; }

        public string LastEditedAt { get; }

        public string UpdatedAt { get; }

        public bool ViewerCanDelete { get; }

        public bool ViewerCanEdit { get; }

        public IQueryable<CommentCannotEditReason> ViewerCannotEditReasons => this.CreateProperty(x => x.ViewerCannotEditReasons);

        public bool ViewerDidAuthor { get; }

        internal static StubIComment Create(IQueryProvider provider, Expression expression)
        {
            return new StubIComment(provider, expression);
        }
    }
}