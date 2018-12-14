using System.Collections.Generic;

namespace Octokit.GraphQL.Core.UnitTests.PagingTests.Models
{
    class RepositoryModelWithDictionary
    {
        public string Name { get; set; }
        public IDictionary<int, IssueModel> Issues { get; set; }
    }
}
