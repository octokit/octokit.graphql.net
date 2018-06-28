namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A repository's open source license
    /// </summary>
    public class License : QueryableValue<License>
    {
        /// <inheritdoc />
        public License(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The full text of the license
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The conditions set by the license
        /// </summary>
        public IQueryableList<LicenseRule> Conditions => this.CreateProperty(x => x.Conditions);

        /// <summary>
        /// A human-readable description of the license
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Whether the license should be featured
        /// </summary>
        public bool Featured { get; }

        /// <summary>
        /// Whether the license should be displayed in license pickers
        /// </summary>
        public bool Hidden { get; }

        public ID Id { get; }

        /// <summary>
        /// Instructions on how to implement the license
        /// </summary>
        public string Implementation { get; }

        /// <summary>
        /// The lowercased SPDX ID of the license
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// The limitations set by the license
        /// </summary>
        public IQueryableList<LicenseRule> Limitations => this.CreateProperty(x => x.Limitations);

        /// <summary>
        /// The license full name specified by <https://spdx.org/licenses>
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Customary short name if applicable (e.g, GPLv3)
        /// </summary>
        public string Nickname { get; }

        /// <summary>
        /// The permissions set by the license
        /// </summary>
        public IQueryableList<LicenseRule> Permissions => this.CreateProperty(x => x.Permissions);

        /// <summary>
        /// Whether the license is a pseudo-license placeholder (e.g., other, no-license)
        /// </summary>
        public bool PseudoLicense { get; }

        /// <summary>
        /// Short identifier specified by <https://spdx.org/licenses>
        /// </summary>
        public string SpdxId { get; }

        /// <summary>
        /// URL to the license on <https://choosealicense.com>
        /// </summary>
        public string Url { get; }

        internal static License Create(Expression expression)
        {
            return new License(expression);
        }
    }
}