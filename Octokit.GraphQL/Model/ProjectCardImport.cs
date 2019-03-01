namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// An issue or PR and its owning repository to be used in a project card.
    /// </summary>
    public class ProjectCardImport
    {
        /// <summary>
        /// Repository name with owner (owner/repository).
        /// </summary>
        public string Repository { get; set; }

        /// <summary>
        /// The issue or pull request number.
        /// </summary>
        public int Number { get; set; }
    }
}