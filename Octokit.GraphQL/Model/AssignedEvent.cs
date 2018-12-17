namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'assigned' event on any assignable object.
    /// </summary>
    public class AssignedEvent : QueryableValue<AssignedEvent>
    {
        internal AssignedEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the assignable associated with the event.
        /// </summary>
        public IAssignable Assignable => this.CreateProperty(x => x.Assignable, Octokit.GraphQL.Model.Internal.StubIAssignable.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public ID Id { get; }

        /// <summary>
        /// Identifies the user who was assigned.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        internal static AssignedEvent Create(Expression expression)
        {
            return new AssignedEvent(expression);
        }
    }
}