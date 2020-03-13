namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A package dependency contains the information needed to satisfy a dependency.
    /// </summary>
    public class RegistryPackageDependency : QueryableValue<RegistryPackageDependency>
    {
        internal RegistryPackageDependency(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the type of dependency.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageDependency` object instead. Removal on 2020-04-01 UTC.")]
        public RegistryPackageDependencyType DependencyType { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the name of the dependency.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageDependency` object instead. Removal on 2020-04-01 UTC.")]
        public string Name { get; }

        /// <summary>
        /// Identifies the version of the dependency.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageDependency` object instead. Removal on 2020-04-01 UTC.")]
        public string Version { get; }

        internal static RegistryPackageDependency Create(Expression expression)
        {
            return new RegistryPackageDependency(expression);
        }
    }
}