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
    /// Represents a type that can be retrieved by a URL.
    /// </summary>
    public interface IUniformResourceLocatable : IQueryableValue<IUniformResourceLocatable>, IQueryableInterface
    {
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

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIUniformResourceLocatable : QueryableValue<StubIUniformResourceLocatable>, IUniformResourceLocatable
    {
        internal StubIUniformResourceLocatable(Expression expression) : base(expression)
        {
        }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string ResourcePath { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string Url { get; }

        internal static StubIUniformResourceLocatable Create(Expression expression)
        {
            return new StubIUniformResourceLocatable(expression);
        }
    }
}