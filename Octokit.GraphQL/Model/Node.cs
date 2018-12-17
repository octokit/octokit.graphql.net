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
    public interface INode : IQueryableValue<INode>, IQueryableInterface
    {
        /// <summary>
        /// ID of the object.
        /// </summary>
        ID Id { get; }
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

    internal class StubINode : QueryableValue<StubINode>, INode
    {
        internal StubINode(Expression expression) : base(expression)
        {
        }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public ID Id { get; }

        internal static StubINode Create(Expression expression)
        {
            return new StubINode(expression);
        }
    }
}