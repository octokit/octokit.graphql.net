namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A threaded list of comments for a given pull request.
    /// </summary>
    public class PullRequestReviewThread : QueryableValue<PullRequestReviewThread>
    {
        public PullRequestReviewThread(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of pull request comments associated with the thread.
        /// </summary>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        public PullRequestReviewCommentConnection Comments(Arg<string>? after = null, Arg<string>? before = null, Arg<int>? first = null, Arg<int>? last = null) => this.CreateMethodCall(x => x.Comments(after, before, first, last), Octokit.GraphQL.Model.PullRequestReviewCommentConnection.Create);

        public ID Id { get; }

        /// <summary>
        /// Identifies the pull request associated with this thread.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        /// <summary>
        /// Identifies the repository associated with this thread.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static PullRequestReviewThread Create(Expression expression)
        {
            return new PullRequestReviewThread(expression);
        }
    }
}