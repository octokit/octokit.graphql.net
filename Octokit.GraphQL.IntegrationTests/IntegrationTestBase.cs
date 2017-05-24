using Octokit.GraphQL.IntegrationTests.Utilities;

namespace Octokit.GraphQL.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        protected Core.Connection Connection;

        protected IntegrationTestBase()
        {
            Connection = new Core.Connection(Helper.GithubComGraphqlApi, Helper.OAuthToken);
        }

        protected static GitHubClient GetV3GitHubClient()
        {
            var credentials = new Credentials(Helper.OAuthToken);
            var gitHubClient = new GitHubClient(new ProductHeaderValue("OctokitTests"), Helper.GithubComApiUri)
            {
                Credentials = credentials
            };
            return gitHubClient;
        }
    }
}