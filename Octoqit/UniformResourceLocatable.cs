namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Represents a type that can be retrieved by a URL.
    /// </summary>
    public interface IUniformResourceLocatable : IQueryEntity
    {
        /// <summary>
        /// The path to this resource.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// The URL to this resource.
        /// </summary>
        string Url { get; }
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubIUniformResourceLocatable : QueryEntity, IUniformResourceLocatable
    {
        public StubIUniformResourceLocatable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Path { get; }

        public string Url { get; }

        internal static StubIUniformResourceLocatable Create(IQueryProvider provider, Expression expression)
        {
            return new StubIUniformResourceLocatable(provider, expression);
        }
    }
}