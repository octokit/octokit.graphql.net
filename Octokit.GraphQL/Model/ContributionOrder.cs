namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for contribution connections.
    /// </summary>
    public class ContributionOrder
    {
        /// <summary>
        /// The field by which to order contributions.
        /// **Upcoming Change on 2019-10-01 UTC**
        /// **Description:** `field` will be removed. Only one order field is supported.
        /// **Reason:** `field` will be removed.
        /// </summary>
        public ContributionOrderField? Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}