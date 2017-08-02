namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An object that can be closed
    /// </summary>
    public interface IClosable : IQueryEntity
    {
        /// <summary>
        /// `true` if the object is closed (definition of closed may depend on type)
        /// </summary>
        bool Closed { get; }
    }
}

namespace Octokit.GraphQL.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIClosable : QueryEntity, IClosable
    {
        public StubIClosable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public bool Closed { get; }

        internal static StubIClosable Create(IQueryProvider provider, Expression expression)
        {
            return new StubIClosable(provider, expression);
        }
    }
}