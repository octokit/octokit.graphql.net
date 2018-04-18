namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for milestone connections.
    /// </summary>
    public class MilestoneOrder
    {
        public MilestoneOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}