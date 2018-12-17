namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a 'base_ref_changed' event on a given issue or pull request.
    /// </summary>
    public class BaseRefChangedEvent : QueryableValue<BaseRefChangedEvent>
    {
        internal BaseRefChangedEvent(Expression expression) : base(expression)
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

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public ID Id { get; }

        internal static BaseRefChangedEvent Create(Expression expression)
        {
            return new BaseRefChangedEvent(expression);
        }
    }
}