namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// An object with an ID.
    /// </summary>
    public interface INode : IQueryEntity
    {
        /// <summary>
        /// ID of the object.
        /// </summary>
        string Id { get; }
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubINode : QueryEntity, INode
    {
        public StubINode(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Id { get; }

        internal static StubINode Create(IQueryProvider provider, Expression expression)
        {
            return new StubINode(provider, expression);
        }
    }
}