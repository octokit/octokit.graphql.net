namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a single registry metadatum
    /// </summary>
    public class RegistryPackageMetadatum
    {
        /// <summary>
        /// Name of the metadatum.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the metadatum.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// True, if the metadatum can be updated if it already exists
        /// </summary>
        public bool? Update { get; set; }
    }
}