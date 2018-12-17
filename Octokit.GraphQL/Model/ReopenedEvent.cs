namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'reopened' event on any `Closable`.
    /// </summary>
    public class ReopenedEvent : QueryableValue<ReopenedEvent>
    {
        internal ReopenedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Object that was reopened.
        /// </summary>
        public IClosable Closable => this.CreateProperty(x => x.Closable, Octokit.GraphQL.Model.Internal.StubIClosable.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public ID Id { get; }

        internal static ReopenedEvent Create(Expression expression)
        {
            return new ReopenedEvent(expression);
        }
    }
}