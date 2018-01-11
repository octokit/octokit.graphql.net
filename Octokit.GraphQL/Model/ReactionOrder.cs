namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;

    /// <summary>
    /// Ways in which lists of reactions can be ordered upon return.
    /// </summary>
    public class ReactionOrder
    {
        public ReactionOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}