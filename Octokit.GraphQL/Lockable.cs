namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An object that can be locked.
    /// </summary>
    public interface ILockable : IQueryEntity
    {
        /// <summary>
        /// `true` if the object is locked
        /// </summary>
        bool Locked { get; }
    }
}

namespace Octokit.GraphQL.Internal
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubILockable : QueryEntity, ILockable
    {
        public StubILockable(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public bool Locked { get; }

        internal static StubILockable Create(IQueryProvider provider, Expression expression)
        {
            return new StubILockable(provider, expression);
        }
    }
}