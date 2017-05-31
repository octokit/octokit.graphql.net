namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'unassigned' event on any assignable object.
    /// </summary>
    public class UnassignedEvent : QueryEntity
    {
        public UnassignedEvent(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the 'unassigned' event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the assignable associated with the event.
        /// </summary>
        public IAssignable Assignable => this.CreateProperty(x => x.Assignable, Octokit.GraphQL.Internal.StubIAssignable.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public string CreatedAt { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the subject (user) who was unassigned.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.User.Create);

        internal static UnassignedEvent Create(IQueryProvider provider, Expression expression)
        {
            return new UnassignedEvent(provider, expression);
        }
    }
}