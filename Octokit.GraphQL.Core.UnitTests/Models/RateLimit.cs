using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class RateLimit : QueryableValue<RateLimit>
    {
        public RateLimit(Expression expression) : base(expression)
        {
        }

        public int Cost { get; }
        public int Limit { get; }
        public int NodeCount { get; }
        public int Remaining { get; }
        public DateTimeOffset ResetAt { get; }

        internal static RateLimit Create(Expression expression)
        {
            return new RateLimit(expression);
        }
    }
}