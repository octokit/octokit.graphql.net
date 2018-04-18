namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for team member connections
    /// </summary>
    public class TeamMemberOrder
    {
        public TeamMemberOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}