using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Deserializers;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests.Queries
{
    public class ErrorTests : IntegrationTestBase
    {
        [IntegrationTest]
        public async Task Should_Throw_Correct_Error_For_Name_Resolution_Failure()
        {
            var query = new Query()
                .Repository(owner: "octokit", name: "octokit.net")
                .Issues(first: 3)
                .Nodes
                .Select(i => new
                {
                    i.Title,
                    RepositoryName = i.Repository.Name,
                });

            var connection = new Connection(
                new ProductHeaderValue("OctokitTests"),
                new Uri("https://invalid.github.123"), 
                Helper.OAuthToken);

            var ex = await Assert.ThrowsAnyAsync<Exception>(async () => await connection.Run(query));

            Assert.IsType<HttpRequestException>(ex);

            // We would like to test this, but there doesn't seem to be an x-plat way to do it.
            // Assert.IsType<WebException>(ex.InnerException);
            // Assert.Equal(WebExceptionStatus.NameResolutionFailure, ((WebException)ex.InnerException).Status);
        }

        [IntegrationTest]
        public async Task Should_Throw_Correct_Error_For_Invalid_Repository_Name()
        {
            var query = new Query()
                .Repository(owner: "octokit", name: "bad_repository")
                .Issues(first: 3)
                .Nodes
                .Select(i => new
                {
                    i.Title,
                    RepositoryName = i.Repository.Name,
                });

            var ex = await Assert.ThrowsAnyAsync<ResponseDeserializerException>(async () => await Connection.Run(query));
            Assert.Equal("Could not resolve to a Repository with the name 'bad_repository'.", ex.Message);
            Assert.Equal(1, ex.Line);
            Assert.Equal(7, ex.Column);
        }
    }
}
