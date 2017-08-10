using System.Linq;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests.Queries
{
    public class PullRequestTests : IntegrationTestBase
    {
        [IntegrationTest]
        public void Should_Query_Commits()
        {
            var query = new GraphQL.Query().Repository("octokit", "octokit.net").PullRequest(1).Commits(3).Nodes
                .Select(pullRequestCommit => new
                {
                    pullRequestCommit.Commit.Id,
                    pullRequestCommit.Commit.Message,
                    pullRequestCommit.Commit.Author.Name
                });

            var results = Enumerable.ToArray(Connection.Run(query).Result);

            Assert.Equal(1, results.Length);
            Assert.Equal((string)"MDY6Q29tbWl0NzUyODY3OTpkYWZhYjhhZjA0ODM5NDU1ODM4Y2QzZmRlMTFkMTM5MTc0MTYyZmFh", (string)results[0].Id);
            var expectedMessage = "Adding README, CONTRIBUTING, LICENSE\n\nWe plan to release this code under the MIT license so might as well get\nthe right things in place early.";
            Assert.Equal(expectedMessage, (string)results[0].Message);
            Assert.Equal((string)"Haacked", (string)results[0].Name);
        }
    }
}