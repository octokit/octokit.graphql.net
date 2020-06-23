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
    /// Entities that can be minimized.
    /// </summary>
    public interface IMinimizable : IQueryableValue<IMinimizable>, IQueryableInterface
    {
        /// <summary>
        /// Returns whether or not a comment has been minimized.
        /// </summary>
        bool IsMinimized { get; }

        /// <summary>
        /// Returns why the comment was minimized.
        /// </summary>
        string MinimizedReason { get; }

        /// <summary>
        /// Check if the current viewer can minimize this object.
        /// </summary>
        bool ViewerCanMinimize { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIMinimizable : QueryableValue<StubIMinimizable>, IMinimizable
    {
        internal StubIMinimizable(Expression expression) : base(expression)
        {
        }

        public bool IsMinimized { get; }

        public string MinimizedReason { get; }

        public bool ViewerCanMinimize { get; }

        internal static StubIMinimizable Create(Expression expression)
        {
            return new StubIMinimizable(expression);
        }
    }
}