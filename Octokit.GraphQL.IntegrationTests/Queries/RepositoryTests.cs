using System.Linq;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests.Queries
{
    public class RepositoryTests : IntegrationTestBase
    {
        [IntegrationTest]
        public void Should_Query_All_RepositoryOwner_Repositories()
        {
            var query = new GraphQL.Query().RepositoryOwner("octokit").Repositories(first: 30).Nodes.Select(repository => repository.Name);

            var repositoryNames = Connection.Run(query).Result.ToArray();

            Assert.Equal(5, repositoryNames.Length);
            Assert.Contains("octokit.net", repositoryNames);
            Assert.Contains("octokit.rb", repositoryNames);
            Assert.Contains("octokit.objc", repositoryNames);
            Assert.Contains("go-octokit", repositoryNames);
            Assert.Contains("octokit.py", repositoryNames);
        }

        [IntegrationTest]
        public void Should_Query_Repository_ByName()
        {
            var query = new GraphQL.Query().Repository("octokit", "octokit.net").Select(r => new
            {
                r.Name,
                r.DatabaseId,
            });

            var repository = Connection.Run(query).Result.FirstOrDefault();

            Assert.NotNull(repository);
            Assert.Equal(repository.Name, "octokit.net");
            Assert.Equal(repository.DatabaseId, 7528679);
        }

        [IntegrationTest]
        public void Should_QueryRepositoryOwner_Repositories_OrderBy_Name_Ascending()
        {
            var query = new GraphQL.Query().RepositoryOwner("octokit").Repositories(first: 30, orderBy: new RepositoryOrder
            {
                Direction = OrderDirection.Asc,
                Field = RepositoryOrderField.Name
            }).Nodes.Select(repository => repository.Name);

            var repositoryNames = Connection.Run(query).Result.ToArray();

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
            var query = new GraphQL.Query().RepositoryOwner("octokit").Repositories(first: 30, orderBy: new RepositoryOrder
            {
                Direction = OrderDirection.Asc,
                Field = RepositoryOrderField.CreatedAt
            }).Nodes.Select(repository => repository.Name);

            var repositoryNames = Connection.Run(query).Result.ToArray();

            Assert.Equal(5, repositoryNames.Length);
            Assert.Equal("octokit.rb", repositoryNames[0]);
            Assert.Equal("octokit.net", repositoryNames[1]);
            Assert.Equal("octokit.objc", repositoryNames[2]);
            Assert.Equal("go-octokit", repositoryNames[3]);
            Assert.Equal("octokit.py", repositoryNames[4]);
        }
    }
}