namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A registry package contains the content for an uploaded package.
    /// </summary>
    public class RegistryPackage : QueryableValue<RegistryPackage>
    {
        internal RegistryPackage(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The package type color
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `Package` object instead. Removal on 2020-04-01 UTC.")]
        public string Color { get; }

        public ID Id { get; }

        /// <summary>
        /// Find the latest version for the package.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `Package` object instead. Removal on 2020-04-01 UTC.")]
        public RegistryPackageVersion LatestVersion => this.CreateProperty(x => x.LatestVersion, Octokit.GraphQL.Model.RegistryPackageVersion.Create);

        /// <summary>
        /// Identifies the title of the package.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `Package` object instead. Removal on 2020-04-01 UTC.")]
        public string Name { get; }

        /// <summary>
        /// Identifies the title of the package, with the owner prefixed.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `Package` object instead. Removal on 2020-04-01 UTC.")]
        public string NameWithOwner { get; }

        /// <summary>
        /// Find the package file identified by the guid.
        /// </summary>
        /// <param name="guid">The unique identifier of the package_file</param>
        public RegistryPackageFile PackageFileByGuid(Arg<string> guid) => this.CreateMethodCall(x => x.PackageFileByGuid(guid), Octokit.GraphQL.Model.RegistryPackageFile.Create);

        /// <summary>
        /// Find the package file identified by the sha256.
        /// </summary>
        /// <param name="sha256">The SHA256 of the package_file</param>
        public RegistryPackageFile PackageFileBySha256(Arg<string> sha256) => this.CreateMethodCall(x => x.PackageFileBySha256(sha256), Octokit.GraphQL.Model.RegistryPackageFile.Create);

        /// <summary>
        /// Identifies the type of the package.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `Package` object instead. Removal on 2020-04-01 UTC.")]
        public RegistryPackageType PackageType { get; }

        /// <summary>
        /// List the prerelease versions for this package.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public RegistryPackageVersionConnection PreReleaseVersions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.PreReleaseVersions(first, after, last, before), Octokit.GraphQL.Model.RegistryPackageVersionConnection.Create);

        /// <summary>
        /// The type of the package.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `Package` object instead. Removal on 2020-04-01 UTC.")]
        public string RegistryPackageType { get; }

        /// <summary>
        /// repository that the release is associated with
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `Package` object instead. Removal on 2020-04-01 UTC.")]
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Statistics about package activity.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `Package` object instead. Removal on 2020-04-01 UTC.")]
        public RegistryPackageStatistics Statistics => this.CreateProperty(x => x.Statistics, Octokit.GraphQL.Model.RegistryPackageStatistics.Create);

        /// <summary>
        /// list of tags for this package
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public RegistryPackageTagConnection Tags(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Tags(first, after, last, before), Octokit.GraphQL.Model.RegistryPackageTagConnection.Create);

        /// <summary>
        /// List the topics for this package.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public TopicConnection Topics(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Topics(first, after, last, before), Octokit.GraphQL.Model.TopicConnection.Create);

        /// <summary>
        /// Find package version by version string.
        /// </summary>
        /// <param name="version">The package version.</param>
        public RegistryPackageVersion Version(Arg<string> version) => this.CreateMethodCall(x => x.Version(version), Octokit.GraphQL.Model.RegistryPackageVersion.Create);

        /// <summary>
        /// Find package version by version string.
        /// </summary>
        /// <param name="platform">Find a registry package for a specific platform.</param>
        /// <param name="version">The package version.</param>
        public RegistryPackageVersion VersionByPlatform(Arg<string> platform, Arg<string> version) => this.CreateMethodCall(x => x.VersionByPlatform(platform, version), Octokit.GraphQL.Model.RegistryPackageVersion.Create);

        /// <summary>
        /// Find package version by manifest SHA256.
        /// </summary>
        /// <param name="sha256">The package SHA256 digest.</param>
        public RegistryPackageVersion VersionBySha256(Arg<string> sha256) => this.CreateMethodCall(x => x.VersionBySha256(sha256), Octokit.GraphQL.Model.RegistryPackageVersion.Create);

        /// <summary>
        /// list of versions for this package
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public RegistryPackageVersionConnection Versions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Versions(first, after, last, before), Octokit.GraphQL.Model.RegistryPackageVersionConnection.Create);

        /// <summary>
        /// List package versions with a specific metadatum.
        /// </summary>
        /// <param name="metadatum">Filter on a specific metadatum.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public RegistryPackageVersionConnection VersionsByMetadatum(Arg<RegistryPackageMetadatum> metadatum, Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.VersionsByMetadatum(metadatum, first, after, last, before), Octokit.GraphQL.Model.RegistryPackageVersionConnection.Create);

        internal static RegistryPackage Create(Expression expression)
        {
            return new RegistryPackage(expression);
        }
    }
}