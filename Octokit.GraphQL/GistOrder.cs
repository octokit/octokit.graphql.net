namespace Octokit.GraphQL
{
    using System.Linq;

    /// <summary>
    /// Ordering options for gist connections
    /// </summary>
    public class GistOrder
    {
        public GistOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}