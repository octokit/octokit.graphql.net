namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a comment.
    /// </summary>
    public interface IComment : IQueryEntity
    {
        /// <summary>
        /// The actor who authored the comment.
        /// </summary>
        IActor Author { get; }

        /// <summary>
        /// Author's association with the subject of the comment.
        /// </summary>
        CommentAuthorAssociation AuthorAssociation { get; }

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
        DateTimeOffset? CreatedAt { get; }

        /// <summary>
        /// Check if this comment was created via an email reply.
        /// </summary>
        bool CreatedViaEmail { get; }

        /// <summary>
        /// The actor who edited the comment.
        /// </summary>
        IActor Editor { get; }

        string Id { get; }

        /// <summary>
        /// The moment the editor made the last edit
        /// </summary>
        DateTimeOffset? LastEditedAt { get; }

        /// <summary>
        /// Identifies when the comment was published at.
        /// </summary>
        DateTimeOffset? PublishedAt { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        DateTimeOffset? UpdatedAt { get; }

        /// <summary>
        /// Did the viewer author this comment.
        /// </summary>
        bool ViewerDidAuthor { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIComment : QueryEntity, IComment
    {
        public StubIComment(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public IActor Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        public CommentAuthorAssociation AuthorAssociation { get; }

        public string Body { get; }

        public string BodyHTML { get; }

        public DateTimeOffset? CreatedAt { get; }

        public bool CreatedViaEmail { get; }

        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        public string Id { get; }

        public DateTimeOffset? LastEditedAt { get; }

        public DateTimeOffset? PublishedAt { get; }

        [Obsolete(@"General type updated timestamps will eventually be replaced by other field specific timestamps.")]
        public DateTimeOffset? UpdatedAt { get; }

        public bool ViewerDidAuthor { get; }

        internal static StubIComment Create(IQueryProvider provider, Expression expression)
        {
            return new StubIComment(provider, expression);
        }
    }
}