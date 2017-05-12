namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

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
        /// The HTML path to this resource.
        /// </summary>
        string ResourcePath { get; }

        /// <summary>
        /// The URL to this resource.
        /// </summary>
        string Url { get; }
    }
}

namespace Octokit.GraphQL.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIUniformResourceLocatable : QueryEntity, IUniformResourceLocatable
    {
        public StubIUniformResourceLocatable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Path { get; }

        public string ResourcePath { get; }

        public string Url { get; }

        internal static StubIUniformResourceLocatable Create(IQueryProvider provider, Expression expression)
        {
            return new StubIUniformResourceLocatable(provider, expression);
        }
    }
}