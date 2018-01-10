namespace Octokit.GraphQL.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A label for categorizing Issues or Milestones with a given Repository.
    /// </summary>
    public class Label : QueryEntity
    {
        public Label(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the label color.
        /// </summary>
        public string Color { get; }

        public string Id { get; }

        /// <summary>
        /// A list of issues associated with this label.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for issues returned from the connection.</param>
        /// <param name="states">A list of states to filter the issues by.</param>
        public IssueConnection Issues(int? first = null, string after = null, int? last = null, string before = null, IEnumerable<string> labels = null, IssueOrder orderBy = null, IEnumerable<IssueState> states = null) => this.CreateMethodCall(x => x.Issues(first, after, last, before, labels, orderBy, states), Octokit.GraphQL.Model.IssueConnection.Create);

        /// <summary>
        /// Identifies the label name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A list of pull requests associated with this label.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        /// <param name="states">A list of states to filter the pull requests by.</param>
        /// <param name="labels">A list of label names to filter the pull requests by.</param>
        /// <param name="headRefName">The head ref name to filter the pull requests by.</param>
        /// <param name="baseRefName">The base ref name to filter the pull requests by.</param>
        /// <param name="orderBy">Ordering options for pull requests returned from the connection.</param>
        public PullRequestConnection PullRequests(int? first = null, string after = null, int? last = null, string before = null, IEnumerable<PullRequestState> states = null, IEnumerable<string> labels = null, string headRefName = null, string baseRefName = null, IssueOrder orderBy = null) => this.CreateMethodCall(x => x.PullRequests(first, after, last, before, states, labels, headRefName, baseRefName, orderBy), Octokit.GraphQL.Model.PullRequestConnection.Create);

        /// <summary>
        /// The repository associated with this label.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static Label Create(IQueryProvider provider, Expression expression)
        {
            return new Label(provider, expression);
        }
    }
}