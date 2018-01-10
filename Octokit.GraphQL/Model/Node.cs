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
    /// An object with an ID.
    /// </summary>
    public interface INode : IQueryEntity
    {
        /// <summary>
        /// ID of the object.
        /// </summary>
        string Id { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubINode : QueryEntity, INode
    {
        public StubINode(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Id { get; }

        internal static StubINode Create(IQueryProvider provider, Expression expression)
        {
            return new StubINode(provider, expression);
        }
    }
}