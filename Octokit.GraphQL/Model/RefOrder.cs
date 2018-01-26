namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which lists of git refs can be ordered upon return.
    /// </summary>
    public class RefOrder
    {
        public RefOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}