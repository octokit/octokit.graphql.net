namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Types that can be inside Project Cards.
    /// </summary>
    public class ProjectCardItem : QueryEntity, IUnion
    {
        public ProjectCardItem(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// An Issue is a place to discuss ideas, enhancements, tasks, and bugs for a project.
        /// </summary>
        public Issue Issue => this.CreateProperty(x => x.Issue, Octoqit.Issue.Create);

        /// <summary>
        /// A repository pull request.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octoqit.PullRequest.Create);

        internal static ProjectCardItem Create(IQueryProvider provider, Expression expression)
        {
            return new ProjectCardItem(provider, expression);
        }
    }
}