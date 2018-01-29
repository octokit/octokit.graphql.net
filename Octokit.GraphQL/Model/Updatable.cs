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
    /// Entities that can be updated.
    /// </summary>
    public interface IUpdatable : IQueryableValue<IUpdatable>, IQueryableInterface
    {
        /// <summary>
        /// Check if the current viewer can update this object.
        /// </summary>
        bool ViewerCanUpdate { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIUpdatable : QueryableValue<StubIUpdatable>, IUpdatable
    {
        public StubIUpdatable(Expression expression) : base(expression)
        {
        }

        public bool ViewerCanUpdate { get; }

        internal static StubIUpdatable Create(Expression expression)
        {
            return new StubIUpdatable(expression);
        }
    }
}