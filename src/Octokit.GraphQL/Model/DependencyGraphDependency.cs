namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A dependency manifest entry
    /// </summary>
    public class DependencyGraphDependency : QueryableValue<DependencyGraphDependency>
    {
        internal DependencyGraphDependency(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Does the dependency itself have dependencies?
        /// </summary>
        public bool HasDependencies { get; }

        /// <summary>
        /// The original name of the package, as it appears in the manifest.
        /// </summary>
        [Obsolete(@"`packageLabel` will be removed. Use normalized `packageName` field instead. Removal on 2022-10-01 UTC.")]
        public string PackageLabel { get; }

        /// <summary>
        /// The dependency package manager
        /// </summary>
        public string PackageManager { get; }

        /// <summary>
        /// The name of the package in the canonical form used by the package manager.
        /// </summary>
        public string PackageName { get; }

        /// <summary>
        /// The repository containing the package
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The dependency version requirements
        /// </summary>
        public string Requirements { get; }

        internal static DependencyGraphDependency Create(Expression expression)
        {
            return new DependencyGraphDependency(expression);
        }
    }
}