namespace Octokit.GraphQL.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Entities that can be updated.
    /// </summary>
    public interface IUpdatable : IQueryEntity
    {
        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        bool ViewerCanUpdate { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIUpdatable : QueryEntity, IUpdatable
    {
        public StubIUpdatable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public bool ViewerCanUpdate { get; }

        internal static StubIUpdatable Create(IQueryProvider provider, Expression expression)
        {
            return new StubIUpdatable(provider, expression);
        }
    }
}