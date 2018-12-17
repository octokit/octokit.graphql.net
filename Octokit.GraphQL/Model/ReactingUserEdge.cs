namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a user that's made a reaction.
    /// </summary>
    public class ReactingUserEdge : QueryableValue<ReactingUserEdge>
    {
        internal ReactingUserEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public User Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The moment when the user made the reaction.
        /// </summary>
        public DateTimeOffset ReactedAt { get; }

        internal static ReactingUserEdge Create(Expression expression)
        {
            return new ReactingUserEdge(expression);
        }
    }
}