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
        string Id { get; }

        /// <summary>
        /// A list of registry packages for a particular search query.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="packageType">Filter registry package by type.</param>
        /// <param name="query">Find registry package by search query.</param>
        RegistryPackageConnection RegistryPackagesForQuery(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<RegistryPackageType>? packageType = null, Arg<string>? query = null);
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

        public string Id { get; }

        public RegistryPackageConnection RegistryPackagesForQuery(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<RegistryPackageType>? packageType = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.RegistryPackagesForQuery(first, after, last, before, packageType, query), Octokit.GraphQL.Model.RegistryPackageConnection.Create);

        internal static StubIRegistryPackageSearch Create(Expression expression)
        {
            return new StubIRegistryPackageSearch(expression);
        }
    }
}