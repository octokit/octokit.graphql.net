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
    public interface IComment : IQueryableValue<IComment>, IQueryableInterface
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
        /// The body as Markdown.
        /// </summary>
        string Body { get; }

        /// <summary>
        /// The body rendered to HTML.
        /// </summary>
        string BodyHTML { get; }

        /// <summary>
        /// The body rendered to text.
        /// </summary>
        string BodyText { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Check if this comment was created via an email reply.
        /// </summary>
        bool CreatedViaEmail { get; }

        /// <summary>
        /// The actor who edited the comment.
        /// </summary>
        IActor Editor { get; }

        ID Id { get; }

        /// <summary>
        /// Check if this comment was edited and includes an edit with the creation data
        /// </summary>
        bool IncludesCreatedEdit { get; }

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
        DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// A list of edits to this content.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        UserContentEditConnection UserContentEdits(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null);

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
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIComment : QueryableValue<StubIComment>, IComment
    {
        internal StubIComment(Expression expression) : base(expression)
        {
        }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public IActor Author => this.CreateProperty(x => x.Author, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public CommentAuthorAssociation AuthorAssociation { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public string Body { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public string BodyHTML { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public string BodyText { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public DateTimeOffset CreatedAt { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public bool CreatedViaEmail { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public IActor Editor => this.CreateProperty(x => x.Editor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public ID Id { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public bool IncludesCreatedEdit { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public DateTimeOffset? LastEditedAt { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public DateTimeOffset? PublishedAt { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public DateTimeOffset UpdatedAt { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public UserContentEditConnection UserContentEdits(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.UserContentEdits(first, after, last, before), Octokit.GraphQL.Model.UserContentEditConnection.Create);

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public bool ViewerDidAuthor { get; }

        internal static StubIComment Create(Expression expression)
        {
            return new StubIComment(expression);
        }
    }
}