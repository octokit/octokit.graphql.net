namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

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
        public IssueConnection Issues(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Issues(first, after, last, before), Octoqit.IssueConnection.Create);

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
        public PullRequestConnection PullRequests(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.PullRequests(first, after, last, before), Octoqit.PullRequestConnection.Create);

        /// <summary>
        /// The repository associated with this label.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        internal static Label Create(IQueryProvider provider, Expression expression)
        {
            return new Label(provider, expression);
        }
    }
}