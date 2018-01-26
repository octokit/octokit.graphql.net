namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'closed' event on any `Closable`.
    /// </summary>
    public class ClosedEvent : QueryableValue<ClosedEvent>
    {
        public ClosedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Object that was closed.
        /// </summary>
        public IClosable Closable => this.CreateProperty(x => x.Closable, Octokit.GraphQL.Model.Internal.StubIClosable.Create);

        /// <summary>
        /// Object which triggered the creation of this event.
        /// </summary>
        public Closer Closer => this.CreateProperty(x => x.Closer, Octokit.GraphQL.Model.Closer.Create);

        /// <summary>
        /// Identifies the commit associated with the 'closed' event.
        /// </summary>
        [Obsolete(@"Use ClosedEvent.closer instead.")]
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; }

        public string Id { get; }

        internal static ClosedEvent Create(Expression expression)
        {
            return new ClosedEvent(expression);
        }
    }
}