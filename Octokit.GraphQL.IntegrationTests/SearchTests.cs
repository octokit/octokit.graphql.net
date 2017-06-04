using System.Linq;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.IntegrationTests.Utilities;

namespace Octokit.GraphQL.IntegrationTests
{
    public class SearchTests : IntegrationTestBase
    {
        [IntegrationTest]
        public void Should_Query_Commits()
        {
            var query = new Query().Search("language:JavaScript stars:>10000", SearchType.Issue, first: 3).Nodes.Select(searchResultItem => new {searchResultItem.Issue.Id});

            var queryBuilder = new QueryBuilder();
            var queryString = queryBuilder.Build(query);;

            //var results = Connection.Run(query).Result.ToArray();
        }
    }
}