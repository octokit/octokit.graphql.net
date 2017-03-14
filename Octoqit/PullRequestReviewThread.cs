namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// A threaded list of comments for a given pull request.
    /// </summary>
    public class PullRequestReviewThread : QueryEntity
    {
        public PullRequestReviewThread(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A list of pull request comments associated with the thread.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public PullRequestReviewCommentConnection Comments(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before), Octoqit.PullRequestReviewCommentConnection.Create);

        public string Id { get; }

        /// <summary>
        /// Identifies the pull request associated with this thread.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octoqit.PullRequest.Create);

        internal static PullRequestReviewThread Create(IQueryProvider provider, Expression expression)
        {
            return new PullRequestReviewThread(provider, expression);
        }
    }
}