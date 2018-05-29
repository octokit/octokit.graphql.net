using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class Milestone : QueryableValue<Milestone>
    {
        public Milestone(Expression expression) : base(expression)
        {
        }

        public bool Closed { get; }
        public string Description { get; }

        internal static Milestone Create(Expression expression)
        {
            return new Milestone(expression);
        }
    }
}