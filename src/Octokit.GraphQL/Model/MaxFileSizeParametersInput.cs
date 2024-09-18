namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Prevent commits that exceed a specified file size limit from being pushed to the commit.
    /// </summary>
    public class MaxFileSizeParametersInput
    {
        /// <summary>
        /// The maximum file size allowed in megabytes. This limit does not apply to Git Large File Storage (Git LFS).
        /// </summary>
        public int MaxFileSize { get; set; }
    }
}