namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which project v2 status updates can be ordered.
    /// </summary>
    public class ProjectV2StatusOrder
    {
        /// <summary>
        /// The field by which to order nodes.
        /// </summary>
        public ProjectV2StatusUpdateOrderField Field { get; set; }

        /// <summary>
        /// The direction in which to order nodes.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}