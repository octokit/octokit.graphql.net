namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A file in a specific registry package version.
    /// </summary>
    public class RegistryPackageFile : QueryableValue<RegistryPackageFile>
    {
        internal RegistryPackageFile(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for this file.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageFile` object instead. Removal on 2020-04-01 UTC.")]
        public string Guid { get; }

        public string Id { get; }

        /// <summary>
        /// Identifies the md5.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageFile` object instead. Removal on 2020-04-01 UTC.")]
        public string Md5 { get; }

        /// <summary>
        /// URL to download the asset metadata.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageFile` object instead. Removal on 2020-04-01 UTC.")]
        public string MetadataUrl { get; }

        /// <summary>
        /// Name of the file
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageFile` object instead. Removal on 2020-04-01 UTC.")]
        public string Name { get; }

        /// <summary>
        /// The package version this file belongs to.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageFile` object instead. Removal on 2020-04-01 UTC.")]
        public RegistryPackageVersion PackageVersion => this.CreateProperty(x => x.PackageVersion, Octokit.GraphQL.Model.RegistryPackageVersion.Create);

        /// <summary>
        /// Identifies the sha1.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageFile` object instead. Removal on 2020-04-01 UTC.")]
        public string Sha1 { get; }

        /// <summary>
        /// Identifies the sha256.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageFile` object instead. Removal on 2020-04-01 UTC.")]
        public string Sha256 { get; }

        /// <summary>
        /// Identifies the size.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageFile` object instead. Removal on 2020-04-01 UTC.")]
        public int? Size { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// URL to download the asset.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageFile` object instead. Removal on 2020-04-01 UTC.")]
        public string Url { get; }

        internal static RegistryPackageFile Create(Expression expression)
        {
            return new RegistryPackageFile(expression);
        }
    }
}