namespace Octokit.GraphQL
{
    using System.Linq;

    /// <summary>
    /// Ways in which lists of issues can be ordered upon return.
    /// </summary>
    public class IssueOrder
    {
        public IssueOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}