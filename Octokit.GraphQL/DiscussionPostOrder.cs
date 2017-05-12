namespace Octokit.GraphQL
{
    using System.Linq;

    /// <summary>
    /// Ways in which discussion post connections can be ordered.
    /// </summary>
    public class DiscussionPostOrder
    {
        public DiscussionPostOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}