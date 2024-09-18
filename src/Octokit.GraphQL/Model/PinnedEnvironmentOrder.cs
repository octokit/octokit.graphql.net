namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for pinned environments
    /// </summary>
    public class PinnedEnvironmentOrder
    {
        /// <summary>
        /// The field to order pinned environments by.
        /// </summary>
        public PinnedEnvironmentOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order pinned environments by the specified field.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}