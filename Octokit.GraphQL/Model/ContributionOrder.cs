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
        /// </summary>
        public ContributionOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}