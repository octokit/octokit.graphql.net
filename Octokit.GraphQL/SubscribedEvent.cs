namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'subscribed' event on a given `Subscribable`.
    /// </summary>
    public class SubscribedEvent : QueryEntity
    {
        public SubscribedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the 'subscribed' event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// Object referenced by event.
        /// </summary>
        public ISubscribable Subscribable => this.CreateProperty(x => x.Subscribable, Octokit.GraphQL.Internal.StubISubscribable.Create);

        internal static SubscribedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new SubscribedEvent(provider, expression);
        }
    }
}