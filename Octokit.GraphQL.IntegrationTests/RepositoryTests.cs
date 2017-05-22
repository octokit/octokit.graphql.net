using System.Linq;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests
{
    public class RepositoryTests
    {
        [IntegrationTest]
        public void Should_Query_Repositories_By_Owner()
        {
            var connection = new Core.Connection(Helper.GithubComGraphqlApi, Helper.OAuthToken);

            var query = new Query().RepositoryOwner("octokit").Repositories(first: 30).Nodes.Select(repository => repository.Name);

            var repositoryNames = connection.Run(query).Result.ToArray();

            Assert.Equal(5, repositoryNames.Length);
            Assert.Contains("octokit.net", repositoryNames);
            Assert.Contains("octokit.rb", repositoryNames);
            Assert.Contains("octokit.objc", repositoryNames);
            Assert.Contains("go-octokit", repositoryNames);
            Assert.Contains("octokit.py", repositoryNames);
        }
    }
}