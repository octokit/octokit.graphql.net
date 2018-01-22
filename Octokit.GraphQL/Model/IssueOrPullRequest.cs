namespace Octokit.GraphQL.Model
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Used for return value of Repository.issueOrPullRequest.
    /// </summary>
    public class IssueOrPullRequest : QueryableValue<IssueOrPullRequest>, IUnion
    {
        public IssueOrPullRequest(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// An Issue is a place to discuss ideas, enhancements, tasks, and bugs for a project.
        /// </summary>
        public Issue Issue => this.CreateProperty(x => x.Issue, Octokit.GraphQL.Model.Issue.Create);

        /// <summary>
        /// A repository pull request.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        internal static IssueOrPullRequest Create(IQueryProvider provider, Expression expression)
        {
            return new IssueOrPullRequest(provider, expression);
        }
    }
}