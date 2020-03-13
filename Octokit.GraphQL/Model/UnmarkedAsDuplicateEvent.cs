namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an 'unmarked_as_duplicate' event on a given issue or pull request.
    /// </summary>
    public class UnmarkedAsDuplicateEvent : QueryableValue<UnmarkedAsDuplicateEvent>
    {
        internal UnmarkedAsDuplicateEvent(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor => this.CreateProperty(x => x.Actor, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        public string Id { get; }

        internal static UnmarkedAsDuplicateEvent Create(Expression expression)
        {
            return new UnmarkedAsDuplicateEvent(expression);
        }
    }
}