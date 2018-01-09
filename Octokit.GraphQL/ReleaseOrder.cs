namespace Octokit.GraphQL
{
    using System.Linq;

    /// <summary>
    /// Ways in which lists of releases can be ordered upon return.
    /// </summary>
    public class ReleaseOrder
    {
        public ReleaseOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}