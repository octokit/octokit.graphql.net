namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A package version contains the information about a specific package version.
    /// </summary>
    public class RegistryPackageVersion : QueryableValue<RegistryPackageVersion>
    {
        internal RegistryPackageVersion(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// list of dependencies for this package
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="type">Find dependencies by type.</param>
        public RegistryPackageDependencyConnection Dependencies(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<RegistryPackageDependencyType>? type = null) => this.CreateMethodCall(x => x.Dependencies(first, after, last, before, type), Octokit.GraphQL.Model.RegistryPackageDependencyConnection.Create);

        /// <summary>
        /// A file associated with this registry package version
        /// </summary>
        /// <param name="filename">A specific file to find.</param>
        public RegistryPackageFile FileByName(Arg<string> filename) => this.CreateMethodCall(x => x.FileByName(filename), Octokit.GraphQL.Model.RegistryPackageFile.Create);

        /// <summary>
        /// List of files associated with this registry package version
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public RegistryPackageFileConnection Files(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Files(first, after, last, before), Octokit.GraphQL.Model.RegistryPackageFileConnection.Create);

        public string Id { get; }

        /// <summary>
        /// A single line of text to install this package version.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public string InstallationCommand { get; }

        /// <summary>
        /// Identifies the package manifest for this package version.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public string Manifest { get; }

        /// <summary>
        /// Identifies the platform this version was built for.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public string Platform { get; }

        /// <summary>
        /// Indicates whether this version is a pre-release.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public bool PreRelease { get; }

        /// <summary>
        /// The README of this package version
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public string Readme { get; }

        /// <summary>
        /// The HTML README of this package version
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public string ReadmeHtml { get; }

        /// <summary>
        /// Registry package associated with this version.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public RegistryPackage RegistryPackage => this.CreateProperty(x => x.RegistryPackage, Octokit.GraphQL.Model.RegistryPackage.Create);

        /// <summary>
        /// Release associated with this package.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public Release Release => this.CreateProperty(x => x.Release, Octokit.GraphQL.Model.Release.Create);

        /// <summary>
        /// Identifies the sha256.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public string Sha256 { get; }

        /// <summary>
        /// Identifies the size.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public int? Size { get; }

        /// <summary>
        /// Statistics about package activity.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public RegistryPackageVersionStatistics Statistics => this.CreateProperty(x => x.Statistics, Octokit.GraphQL.Model.RegistryPackageVersionStatistics.Create);

        /// <summary>
        /// Identifies the package version summary.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public string Summary { get; }

        /// <summary>
        /// Time at which the most recent file upload for this package version finished
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// Identifies the version number.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public string Version { get; }

        /// <summary>
        /// Can the current viewer edit this Package version.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersion` object instead. Removal on 2020-04-01 UTC.")]
        public bool ViewerCanEdit { get; }

        internal static RegistryPackageVersion Create(Expression expression)
        {
            return new RegistryPackageVersion(expression);
        }
    }
}