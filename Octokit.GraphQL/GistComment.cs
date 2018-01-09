namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a comment on an Gist.
    /// </summary>
    public class GistComment : QueryEntity
    {
        public GistComment(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        public IActor Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Author's association with the gist.
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
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// Check if this comment was created via an email reply.
        /// </summary>
        public bool CreatedViaEmail { get; }

        /// <summary>
        /// The actor who edited the comment.
        /// </summary>
        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// The associated gist.
        /// </summary>
        public Gist Gist => this.CreateProperty(x => x.Gist, Octokit.GraphQL.Gist.Create);

        public string Id { get; }

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        public string LastEditedAt { get; }

        /// <summary>
        /// Identifies when the comment was published at.
        /// </summary>
        public string PublishedAt { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public string UpdatedAt { get; }

        /// <summary>
        /// Check if the current viewer can delete this object.
        /// </summary>
        public bool ViewerCanDelete { get; }

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

        internal static GistComment Create(IQueryProvider provider, Expression expression)
        {
            return new GistComment(provider, expression);
        }
    }
}