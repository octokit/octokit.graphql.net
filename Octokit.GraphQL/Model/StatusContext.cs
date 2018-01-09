namespace Octokit.GraphQL.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

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
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// The name of this status context.
        /// </summary>
        public string Context { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        /// <summary>
        /// The actor who created this status context.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

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
        public string TargetUrl { get; }

        internal static StatusContext Create(IQueryProvider provider, Expression expression)
        {
            return new StatusContext(provider, expression);
        }
    }
}