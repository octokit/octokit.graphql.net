namespace Octoqit
{
    using System.Linq;

    /// <summary>
    /// Ordering options for language connections.
    /// </summary>
    public class LanguageOrder
    {
        public LanguageOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}