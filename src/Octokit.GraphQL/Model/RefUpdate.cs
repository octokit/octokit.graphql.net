namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A ref update
    /// </summary>
    public class RefUpdate
    {
        /// <summary>
        /// The fully qualified name of the ref to be update. For example `refs/heads/branch-name`
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value this ref should be updated to.
        /// </summary>
        public string AfterOid { get; set; }

        /// <summary>
        /// The value this ref needs to point to before the update.
        /// </summary>
        public string BeforeOid { get; set; }

        /// <summary>
        /// Force a non fast-forward update.
        /// </summary>
        public bool? Force { get; set; }
    }
}