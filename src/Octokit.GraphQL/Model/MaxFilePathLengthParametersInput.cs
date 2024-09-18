namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Prevent commits that include file paths that exceed a specified character limit from being pushed to the commit graph.
    /// </summary>
    public class MaxFilePathLengthParametersInput
    {
        /// <summary>
        /// The maximum amount of characters allowed in file paths
        /// </summary>
        public int MaxFilePathLength { get; set; }
    }
}