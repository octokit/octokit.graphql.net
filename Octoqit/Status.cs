namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a commit status.
    /// </summary>
    public class Status : QueryEntity
    {
        public Status(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The commit this status is attached to.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octoqit.Commit.Create);

        /// <summary>
        /// Looks up an individual status context by context name.
        /// </summary>
        /// <param name="name">The context name.</param>
        public StatusContext Context(string name) => this.CreateMethodCall(x => x.Context(name), Octoqit.StatusContext.Create);

        /// <summary>
        /// The individual status contexts for this commit.
        /// </summary>
        public IQueryable<StatusContext> Contexts => this.CreateProperty(x => x.Contexts);

        public string Id { get; }

        /// <summary>
        /// The combined commit status.
        /// </summary>
        public StatusState State { get; }

        internal static Status Create(IQueryProvider provider, Expression expression)
        {
            return new Status(provider, expression);
        }
    }
}