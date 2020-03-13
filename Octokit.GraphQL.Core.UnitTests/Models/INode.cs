using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public interface INode : IQueryableInterface
    {
        string Id { get; }
    }

    internal class StubINode : QueryableValue<StubINode>, INode
    {
        public StubINode(Expression expression) : base(expression)
        {
        }

        public string Id { get; }

        internal static StubINode Create(Expression expression)
        {
            return new StubINode(expression);
        }
    }

}
