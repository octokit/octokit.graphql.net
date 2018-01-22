namespace Octokit.GraphQL.Model
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can be inside a Milestone.
    /// </summary>
    public class MilestoneItem : QueryableValue<MilestoneItem>, IUnion
    {
        public MilestoneItem(Expression expression) : base(expression)
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

        internal static MilestoneItem Create(Expression expression)
        {
            return new MilestoneItem(expression);
        }
    }
}