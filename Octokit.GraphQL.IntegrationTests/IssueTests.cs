
using System.Linq;
using Octokit.GraphQL.IntegrationTests.Utilities;

namespace Octokit.GraphQL.IntegrationTests
{
    public class IssueTests : IntegrationTestBase
    {
        [IntegrationTest]
        public void Should_Query_Issues_By_Repository()
        {
            var query = new Query().Repository("octokit", "octokit.net").Issues(first: 3).Nodes.Select(i => i.Title);
            var result = Enumerable.ToArray<string>(Connection.Run(query).Result);
        }

        [IntegrationTest]
        public void Should_Query_Issues_By_State_And_Repository()
        {
            var openState = new IssueState?[] { IssueState.Open }.AsQueryable();
            var query = new Query().Repository("octokit", "octokit.net").Issues(first: 3, states: openState).Nodes.Select(i => i.Title);
            var result = Enumerable.ToArray<string>(Connection.Run(query).Result);
        }
    }
}