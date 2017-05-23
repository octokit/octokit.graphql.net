using System.Linq;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests
{
    public class RepositoryTests
    {
        [IntegrationTest]
        public void Should_Query_All_RepositoryOwner_Repositories()
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

        [IntegrationTest]
        public void Should_QueryRepositoryOwner_Repositories_OrderBy_Name_Ascending()
        {
            var connection = new Core.Connection(Helper.GithubComGraphqlApi, Helper.OAuthToken);

            var query = new Query().RepositoryOwner("octokit").Repositories(first: 30, orderBy: new RepositoryOrder
            {
                Direction = OrderDirection.Asc,
                Field = RepositoryOrderField.Name
            }).Nodes.Select(repository => repository.Name);

            var repositoryNames = connection.Run(query).Result.ToArray();

            Assert.Equal(5, repositoryNames.Length);
            Assert.Equal("go-octokit", repositoryNames[0]);
            Assert.Equal("octokit.net", repositoryNames[1]);
            Assert.Equal("octokit.objc", repositoryNames[2]);
            Assert.Equal("octokit.py", repositoryNames[3]);
            Assert.Equal("octokit.rb", repositoryNames[4]);
        }

        [IntegrationTest]
        public void Should_QueryRepositoryOwner_Repositories_OrderBy_CreatedAt_Descending()
        {
            var connection = new Core.Connection(Helper.GithubComGraphqlApi, Helper.OAuthToken);

            var query = new Query().RepositoryOwner("octokit").Repositories(first: 30, orderBy: new RepositoryOrder
            {
                Direction = OrderDirection.Asc,
                Field = RepositoryOrderField.CreatedAt
            }).Nodes.Select(repository => repository.Name);

            var repositoryNames = connection.Run(query).Result.ToArray();

            Assert.Equal(5, repositoryNames.Length);
            Assert.Equal("octokit.rb", repositoryNames[0]);
            Assert.Equal("octokit.net", repositoryNames[1]);
            Assert.Equal("octokit.objc", repositoryNames[2]);
            Assert.Equal("go-octokit", repositoryNames[3]);
            Assert.Equal("octokit.py", repositoryNames[4]);
        }
    }
}