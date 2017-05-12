namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a Git commit part of a pull request.
    /// </summary>
    public class PullRequestCommit : QueryEntity
    {
        public PullRequestCommit(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The Git commit object
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Commit.Create);

        public string Id { get; }

        /// <summary>
        /// The pull request this commit belongs to
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.PullRequest.Create);

        internal static PullRequestCommit Create(IQueryProvider provider, Expression expression)
        {
            return new PullRequestCommit(provider, expression);
        }
    }
}