namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which star connections can be ordered.
    /// </summary>
    public class StarOrder
    {
        public StarOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}