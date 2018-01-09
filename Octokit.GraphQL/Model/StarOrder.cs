namespace Octokit.GraphQL.Model
{
    using System.Linq;

    /// <summary>
    /// Ways in which star connections can be ordered.
    /// </summary>
    public class StarOrder
    {
        public StarOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}