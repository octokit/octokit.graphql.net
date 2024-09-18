namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Prevent commits that include changes in specified file paths from being pushed to the commit graph.
    /// </summary>
    public class FilePathRestrictionParametersInput
    {
        /// <summary>
        /// The file paths that are restricted from being pushed to the commit graph.
        /// </summary>
        public IEnumerable<string> RestrictedFilePaths { get; set; }
    }
}