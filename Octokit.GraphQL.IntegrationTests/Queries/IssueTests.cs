using System.Linq;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests.Queries
{
    public class IssueTests : IntegrationTestBase
    {
        [IntegrationTest]
        public void Should_Query_Issues_By_Repository()
        {
            var query = new GraphQL.Query().Repository("octokit", "octokit.net").Issues(first: 3).Nodes.Select(i => new
            {
                i.Title,
                RepositoryName = i.Repository.Name,
            });

            var results = Connection.Run(query).Result.ToArray();
            foreach (var result in results)
            {
                Assert.Equal("octokit.net", result.RepositoryName);
            }
        }

        [IntegrationTest]
        public void Should_Query_Issues_By_State_And_Repository()
        {
            var openState = new[] { IssueState.Closed }.AsQueryable();
            var query = new GraphQL.Query().Repository("octokit", "octokit.net").Issues(first: 3, states: openState).Nodes.Select(i => new
            {
                i.Title,
                i.State,
                RepositoryName = i.Repository.Name,
            });

            var results = Connection.Run(query).Result.ToArray();
            foreach (var result in results)
            {
                Assert.Equal("octokit.net", result.RepositoryName);
                Assert.Equal(IssueState.Closed, result.State);
            }
        }
    }
}