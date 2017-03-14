namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Shared features of Issues and Pull Requests.
    /// </summary>
    public interface IIssueish : IQueryEntity
    {
        /// <summary>
        /// A list of Users assigned to the Issue or Pull Request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        UserConnection Assignees(int? first = null, string after = null, int? last = null, string before = null);

        /// <summary>
        /// The author of the issue or pull request.
        /// </summary>
        IAuthor Author { get; }

        /// <summary>
        /// Identifies the body of the issue.
        /// </summary>
        string Body { get; }

        /// <summary>
        /// Identifies the body of the issue rendered to HTML.
        /// </summary>
        string BodyHTML { get; }

        string Id { get; }

        /// <summary>
        /// A list of labels associated with the Issue or Pull Request.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        LabelConnection Labels(int? first = null, string after = null, int? last = null, string before = null);

        /// <summary>
        /// Identifies the issue number.
        /// </summary>
        int Number { get; }

        /// <summary>
        /// Identifies the repository associated with the issue.
        /// </summary>
        Repository Repository { get; }

        /// <summary>
        /// Identifies the issue title.
        /// </summary>
        string Title { get; }
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubIIssueish : QueryEntity, IIssueish
    {
        public StubIIssueish(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public UserConnection Assignees(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Assignees(first, after, last, before), Octoqit.UserConnection.Create);

        public IAuthor Author => this.CreateProperty(x => x.Author, Octoqit.Internal.StubIAuthor.Create);

        public string Body { get; }

        public string BodyHTML { get; }

        public string Id { get; }

        public LabelConnection Labels(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before), Octoqit.LabelConnection.Create);

        public int Number { get; }

        public Repository Repository => this.CreateProperty(x => x.Repository, Octoqit.Repository.Create);

        public string Title { get; }

        internal static StubIIssueish Create(IQueryProvider provider, Expression expression)
        {
            return new StubIIssueish(provider, expression);
        }
    }
}