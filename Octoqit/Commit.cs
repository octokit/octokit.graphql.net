namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a Git commit.
    /// </summary>
    public class Commit : QueryEntity
    {
        public Commit(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Authorship details of the commit.
        /// </summary>
        public GitActor Author => this.CreateProperty(x => x.Author, Octoqit.GitActor.Create);

        /// <summary>
        /// Fetches `git blame` information.
        /// </summary>
        /// <param name="path">The file whose Git blame information you want.</param>
        public Blame Blame(string path) => this.CreateMethodCall(x => x.Blame(path), Octoqit.Blame.Create);

        /// <summary>
        /// Comments made on the commit.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public CommitCommentConnection Comments(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before), Octoqit.CommitCommentConnection.Create);

        /// <summary>
        /// Check if commited via GitHub web UI.
        /// </summary>
        public bool CommittedViaWeb { get; }

        /// <summary>
        /// Committership details of the commit.
        /// </summary>
        public GitActor Committer => this.CreateProperty(x => x.Committer, Octoqit.GitActor.Create);

        /// <summary>
        /// The linear commit history starting from (and including) this commit, in the same order as `git log`.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="path">If non-null, filters history to only show commits touching files under this path.</param>
        /// <param name="author">If non-null, filters history to only show commits with matching authorship.</param>
        /// <param name="since">Allows specifying a beginning time or date for fetching commits.</param>
        /// <param name="until">Allows specifying an ending time or date for fetching commits.</param>
        public CommitHistoryConnection History(int? first = null, string after = null, int? last = null, string before = null, string path = null, CommitAuthor author = null, string since = null, string until = null) => this.CreateMethodCall(x => x.History(first, after, last, before, path, author, since, until), Octoqit.CommitHistoryConnection.Create);

        public string Id { get; }

        /// <summary>
        /// The Git commit message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The Git commit message body
        /// </summary>
        public string MessageBody { get; }

        /// <summary>
        /// The commit message body rendered to HTML.
        /// </summary>
        public string MessageBodyHTML { get; }

        /// <summary>
        /// The Git commit message headline
        /// </summary>
        public string MessageHeadline { get; }

        /// <summary>
        /// The commit message headline rendered to HTML.
        /// </summary>
        public string MessageHeadlineHTML { get; }

        /// <summary>
        /// The Git object ID
        /// </summary>
        public string Oid { get; }

        /// <summary>
        /// The HTTP path for this commit
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The Repository this commit belongs to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        /// <summary>
        /// Commit signing information, if present.
        /// </summary>
        public IGitSignature Signature => this.CreateProperty(x => x.Signature, Octoqit.Internal.StubIGitSignature.Create);

        /// <summary>
        /// Status information for this commit
        /// </summary>
        public Status Status => this.CreateProperty(x => x.Status, Octoqit.Status.Create);

        /// <summary>
        /// Commit's root Tree
        /// </summary>
        public Tree Tree => this.CreateProperty(x => x.Tree, Octoqit.Tree.Create);

        /// <summary>
        /// The HTTP url for this commit
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// The websocket channel ID for live updates.
        /// </summary>
        public string Websocket { get; }

        internal static Commit Create(IQueryProvider provider, Expression expression)
        {
            return new Commit(provider, expression);
        }
    }
}