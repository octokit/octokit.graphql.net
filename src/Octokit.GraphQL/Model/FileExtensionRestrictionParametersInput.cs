namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Prevent commits that include files with specified file extensions from being pushed to the commit graph.
    /// </summary>
    public class FileExtensionRestrictionParametersInput
    {
        /// <summary>
        /// The file extensions that are restricted from being pushed to the commit graph.
        /// </summary>
        public IEnumerable<string> RestrictedFileExtensions { get; set; }
    }
}