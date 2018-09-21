namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of issues can be ordered upon return.
    /// </summary>
    public class PullRequestOrder
    {
        public PullRequestOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}