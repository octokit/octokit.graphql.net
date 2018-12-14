namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an individual commit status context
    /// </summary>
    public class StatusContext : QueryableValue<StatusContext>
    {
        internal StatusContext(Expression expression) : base(expression)
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
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The actor who created this status context.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// The description for this status context.
        /// </summary>
        public string Description { get; }

        public ID Id { get; }

        /// <summary>
        /// The state of this status context.
        /// </summary>
        public StatusState State { get; }

        /// <summary>
        /// The URL for this status context.
        /// </summary>
        public string TargetUrl { get; }

        internal static StatusContext Create(Expression expression)
        {
            return new StatusContext(expression);
        }
    }
}