namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of UnsubscribeFromNotifications
    /// </summary>
    public class UnsubscribeFromNotificationsPayload : QueryableValue<UnsubscribeFromNotificationsPayload>
    {
        internal UnsubscribeFromNotificationsPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// Did the operation succeed?
        /// </summary>
        public bool? Success { get; }

        internal static UnsubscribeFromNotificationsPayload Create(Expression expression)
        {
            return new UnsubscribeFromNotificationsPayload(expression);
        }
    }
}