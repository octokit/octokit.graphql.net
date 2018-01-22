namespace Octokit.GraphQL.Model
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The object which triggered a `ClosedEvent`.
    /// </summary>
    public class Closer : QueryableValue<Closer>, IUnion
    {
        public Closer(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Represents a Git commit.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// A repository pull request.
        /// </summary>
        public PullRequest PullRequest => this.CreateProperty(x => x.PullRequest, Octokit.GraphQL.Model.PullRequest.Create);

        internal static Closer Create(IQueryProvider provider, Expression expression)
        {
            return new Closer(provider, expression);
        }
    }
}