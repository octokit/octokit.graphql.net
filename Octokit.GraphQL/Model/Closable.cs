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
    /// An object that can be closed
    /// </summary>
    public interface IClosable : IQueryableValue<IClosable>
    {
        /// <summary>
        /// `true` if the object is closed (definition of closed may depend on type)
        /// </summary>
        bool Closed { get; }

        /// <summary>
        /// Identifies the date and time when the object was closed.
        /// </summary>
        DateTimeOffset? ClosedAt { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIClosable : QueryableValue<StubIClosable>, IClosable
    {
        public StubIClosable(Expression expression) : base(expression)
        {
        }

        public bool Closed { get; }

        public DateTimeOffset? ClosedAt { get; }

        internal static StubIClosable Create(Expression expression)
        {
            return new StubIClosable(expression);
        }
    }
}