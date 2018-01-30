namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for repository connections
    /// </summary>
    public class RepositoryOrder
    {
        public RepositoryOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}