using System.Linq;
using Xunit;

namespace Octokit.GraphQL.Core.IntegrationTests
{
    public class ViewerTests
    {
        [IntegrationTest]
        public void Viewer_By_OAuthToken_Matches_Username()
        {
            var connection = new Connection("https://api.github.com/graphql", Helper.OAuthToken);
            var query = new Query().Viewer.Select(user => user.Login);

            var login = connection.Run(query).Result.FirstOrDefault();
            Assert.Equal(Helper.Username, login);
        }
    }
}