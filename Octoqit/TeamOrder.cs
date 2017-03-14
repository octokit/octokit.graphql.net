namespace Octoqit
{
    using System.Linq;

    /// <summary>
    /// Ways in which team connections can be ordered.
    /// </summary>
    public class TeamOrder
    {
        public TeamOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}