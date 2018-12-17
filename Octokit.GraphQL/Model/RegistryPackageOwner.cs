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
    /// Represents an owner of a registry package.
    /// </summary>
    public interface IRegistryPackageOwner : IQueryableValue<IRegistryPackageOwner>, IQueryableInterface
    {
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

    internal class StubIRegistryPackageOwner : QueryableValue<StubIRegistryPackageOwner>, IRegistryPackageOwner
    {
        internal StubIRegistryPackageOwner(Expression expression) : base(expression)
        {
        }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public ID Id { get; }

        internal static StubIRegistryPackageOwner Create(Expression expression)
        {
            return new StubIRegistryPackageOwner(expression);
        }
    }
}