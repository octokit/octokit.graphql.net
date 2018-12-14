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
    /// Represents an interface to search packages on an object.
    /// </summary>
    public interface IRegistryPackageSearch : IQueryableValue<IRegistryPackageSearch>, IQueryableInterface
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

    internal class StubIRegistryPackageSearch : QueryableValue<StubIRegistryPackageSearch>, IRegistryPackageSearch
    {
        internal StubIRegistryPackageSearch(Expression expression) : base(expression)
        {
        }

        public ID Id { get; }

        internal static StubIRegistryPackageSearch Create(Expression expression)
        {
            return new StubIRegistryPackageSearch(expression);
        }
    }
}