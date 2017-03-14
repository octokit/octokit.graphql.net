namespace Octoqit
{
    using System.Linq;

    /// <summary>
    /// Ways in which member connections can be ordered.
    /// </summary>
    public class MemberOrder
    {
        public UserOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}