using Octokit.GraphQL.IntegrationTests.Utilities;

namespace Octokit.GraphQL.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        protected IConnection Connection;

        protected IntegrationTestBase()
        {
            Connection = new Connection(new ProductHeaderValue("OctokitTests"), Helper.OAuthToken);
        }

        protected static GitHubClient GetV3GitHubClient()
        {
            var credentials = new Credentials(Helper.OAuthToken);
            var gitHubClient = new GitHubClient(new Octokit.ProductHeaderValue("OctokitTests"))
            {
                Credentials = credentials
            };
            return gitHubClient;
        }
    }
}