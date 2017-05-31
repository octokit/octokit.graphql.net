namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents the client's rate limit.
    /// </summary>
    public class RateLimit : QueryEntity
    {
        public RateLimit(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The point cost for the current query counting against the rate limit.
        /// </summary>
        public int Cost { get; }

        /// <summary>
        /// The maximum number of points the client is permitted to consume in a 60 minute window.
        /// </summary>
        public int Limit { get; }

        /// <summary>
        /// The number of points remaining in the current rate limit window.
        /// </summary>
        public int Remaining { get; }

        /// <summary>
        /// The time at which the current rate limit window resets in UTC epoch seconds.
        /// </summary>
        public string ResetAt { get; }

        internal static RateLimit Create(IQueryProvider provider, Expression expression)
        {
            return new RateLimit(provider, expression);
        }
    }
}