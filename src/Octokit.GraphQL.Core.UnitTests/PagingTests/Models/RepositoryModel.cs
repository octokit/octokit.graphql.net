using System.Collections.Generic;

namespace Octokit.GraphQL.Core.UnitTests.PagingTests.Models
{
    class RepositoryModel
    {
        public string Name { get; set; }
        public IList<IssueModel> Issues { get; set; }
    }
}
