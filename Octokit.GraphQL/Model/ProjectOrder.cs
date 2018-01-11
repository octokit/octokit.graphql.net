namespace Octokit.GraphQL.Model
{
    using System;
    using System.Linq;

    /// <summary>
    /// Ways in which lists of projects can be ordered upon return.
    /// </summary>
    public class ProjectOrder
    {
        public ProjectOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}