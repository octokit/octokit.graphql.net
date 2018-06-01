using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class Reaction : QueryableValue<Reaction>
    {
        public Reaction(Expression expression) : base(expression)
        {
        }

        public DateTimeOffset CreatedAt { get; }
        public int? DatabaseId { get; }
        public ID Id { get; }

        internal static Reaction Create(Expression expression)
        {
            return new Reaction(expression);
        }
    }
}