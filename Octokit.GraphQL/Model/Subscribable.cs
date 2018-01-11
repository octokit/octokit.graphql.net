namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Entities that can be subscribed to for web and email notifications.
    /// </summary>
    public interface ISubscribable : IQueryEntity
    {
        string Id { get; }

        /// <summary>
        /// Check if the viewer is able to change their subscription status for the repository.
        /// </summary>
        bool ViewerCanSubscribe { get; }

        /// <summary>
        /// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
        /// </summary>
        SubscriptionState ViewerSubscription { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubISubscribable : QueryEntity, ISubscribable
    {
        public StubISubscribable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Id { get; }

        public bool ViewerCanSubscribe { get; }

        public SubscriptionState ViewerSubscription { get; }

        internal static StubISubscribable Create(IQueryProvider provider, Expression expression)
        {
            return new StubISubscribable(provider, expression);
        }
    }
}