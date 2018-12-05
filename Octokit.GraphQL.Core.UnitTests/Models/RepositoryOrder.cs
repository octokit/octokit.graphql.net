namespace Octokit.GraphQL.Core.UnitTests.Models
{
    /// <summary>
    /// Ordering options for repository connections
    /// </summary>
    public class RepositoryOrder
    {
        public RepositoryOrderField Field { get; set; }

        public OrderDirection Direction { get; set; }
    }
}