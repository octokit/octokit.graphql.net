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
    /// Entities that can be deleted.
    /// </summary>
    public interface IDeletable : IQueryableValue<IDeletable>, IQueryableInterface
    {
        /// <summary>
        /// Check if the current viewer can delete this object.
        /// </summary>
        bool ViewerCanDelete { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIDeletable : QueryableValue<StubIDeletable>, IDeletable
    {
        internal StubIDeletable(Expression expression) : base(expression)
        {
        }

        public bool ViewerCanDelete { get; }

        internal static StubIDeletable Create(Expression expression)
        {
            return new StubIDeletable(expression);
        }
    }
}