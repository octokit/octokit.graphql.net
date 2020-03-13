namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A version tag contains the mapping between a tag name and a version.
    /// </summary>
    public class RegistryPackageTag : QueryableValue<RegistryPackageTag>
    {
        internal RegistryPackageTag(Expression expression) : base(expression)
        {
        }

        public string Id { get; }

        /// <summary>
        /// Identifies the tag name of the version.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageTag` object instead. Removal on 2020-04-01 UTC.")]
        public string Name { get; }

        /// <summary>
        /// version that the tag is associated with
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageTag` object instead. Removal on 2020-04-01 UTC.")]
        public RegistryPackageVersion Version => this.CreateProperty(x => x.Version, Octokit.GraphQL.Model.RegistryPackageVersion.Create);

        internal static RegistryPackageTag Create(Expression expression)
        {
            return new RegistryPackageTag(expression);
        }
    }
}