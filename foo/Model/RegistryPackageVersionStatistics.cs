namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a object that contains package version activity statistics such as downloads.
    /// </summary>
    public class RegistryPackageVersionStatistics : QueryableValue<RegistryPackageVersionStatistics>
    {
        internal RegistryPackageVersionStatistics(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Number of times the package was downloaded this month.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersionStatistics` object instead. Removal on 2020-04-01 UTC.")]
        public int DownloadsThisMonth { get; }

        /// <summary>
        /// Number of times the package was downloaded this week.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersionStatistics` object instead. Removal on 2020-04-01 UTC.")]
        public int DownloadsThisWeek { get; }

        /// <summary>
        /// Number of times the package was downloaded this year.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersionStatistics` object instead. Removal on 2020-04-01 UTC.")]
        public int DownloadsThisYear { get; }

        /// <summary>
        /// Number of times the package was downloaded today.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersionStatistics` object instead. Removal on 2020-04-01 UTC.")]
        public int DownloadsToday { get; }

        /// <summary>
        /// Number of times the package was downloaded since it was created.
        /// </summary>
        [Obsolete(@"Renaming GitHub Packages fields and objects. Use the `PackageVersionStatistics` object instead. Removal on 2020-04-01 UTC.")]
        public int DownloadsTotalCount { get; }

        internal static RegistryPackageVersionStatistics Create(Expression expression)
        {
            return new RegistryPackageVersionStatistics(expression);
        }
    }
}