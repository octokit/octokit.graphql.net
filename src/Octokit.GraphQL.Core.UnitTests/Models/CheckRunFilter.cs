namespace Octokit.GraphQL.Core.UnitTests.Models
{
    /// <summary>
    /// The filters that are available when fetching check runs.
    /// </summary>
    public class CheckRunFilter
    {
        public CheckRunType? CheckType { get; set; }

        public int? AppId { get; set; }

        public string CheckName { get; set; }

        public CheckStatusState? Status { get; set; }
    }
}