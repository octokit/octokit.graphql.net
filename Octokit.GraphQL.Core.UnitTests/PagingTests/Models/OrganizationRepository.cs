using System.Collections.Generic;

namespace Octokit.GraphQL.Core.UnitTests.PagingTests.Models
{
    class OrganizationRepository
    {
        public List<RepositoryListItemModel> Repositories { get; set; }
        public string Name { get; set; }
    }
}
