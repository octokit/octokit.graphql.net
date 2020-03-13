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
        string Id { get; }

        /// <summary>
        /// A list of registry packages under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="name">Find registry package by name.</param>
        /// <param name="names">Find registry packages by their names.</param>
        /// <param name="packageType">Filter registry package by type.</param>
        /// <param name="publicOnly">Filter registry package by whether it is publicly visible</param>
        /// <param name="registryPackageType">Filter registry package by type (string).</param>
        /// <param name="repositoryId">Find registry packages in a repository.</param>
        RegistryPackageConnection RegistryPackages(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? name = null, Arg<IEnumerable<string>>? names = null, Arg<RegistryPackageType>? packageType = null, Arg<bool>? publicOnly = null, Arg<string>? registryPackageType = null, Arg<string>? repositoryId = null);
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIRegistryPackageOwner : QueryableValue<StubIRegistryPackageOwner>, IRegistryPackageOwner
    {
        internal StubIRegistryPackageOwner(Expression expression) : base(expression)
        {
        }

        public string Id { get; }

        public RegistryPackageConnection RegistryPackages(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? name = null, Arg<IEnumerable<string>>? names = null, Arg<RegistryPackageType>? packageType = null, Arg<bool>? publicOnly = null, Arg<string>? registryPackageType = null, Arg<string>? repositoryId = null) => this.CreateMethodCall(x => x.RegistryPackages(first, after, last, before, name, names, packageType, publicOnly, registryPackageType, repositoryId), Octokit.GraphQL.Model.RegistryPackageConnection.Create);

        internal static StubIRegistryPackageOwner Create(Expression expression)
        {
            return new StubIRegistryPackageOwner(expression);
        }
    }
}