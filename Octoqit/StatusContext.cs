namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents an individual commit status context
    /// </summary>
    public class StatusContext : QueryEntity
    {
        public StatusContext(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// This commit this status context is attached to.
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octoqit.Commit.Create);

        /// <summary>
        /// The name of this status context.
        /// </summary>
        public string Context { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// The author of this status context.
        /// </summary>
        public IAuthor Creator => this.CreateProperty(x => x.Creator, Octoqit.Internal.StubIAuthor.Create);

        /// <summary>
        /// The description for this status context.
        /// </summary>
        public string Description { get; }

        public string Id { get; }

        /// <summary>
        /// The state of this status context.
        /// </summary>
        public StatusState State { get; }

        /// <summary>
        /// The URL for this status context.
        /// </summary>
        public string TargetURL { get; }

        internal static StatusContext Create(IQueryProvider provider, Expression expression)
        {
            return new StatusContext(provider, expression);
        }
    }
}