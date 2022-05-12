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
    /// Represents an owner of a project (beta).
    /// </summary>
    public interface IProjectV2Owner : IQueryableValue<IProjectV2Owner>, IQueryableInterface
    {
        ID Id { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIProjectV2Owner : QueryableValue<StubIProjectV2Owner>, IProjectV2Owner
    {
        internal StubIProjectV2Owner(Expression expression) : base(expression)
        {
        }

        public ID Id { get; }

        internal static StubIProjectV2Owner Create(Expression expression)
        {
            return new StubIProjectV2Owner(expression);
        }
    }
}