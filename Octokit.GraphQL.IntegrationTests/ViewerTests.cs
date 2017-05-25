using System;
using System.Linq;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests
{
    public class ViewerTests: IntegrationTestBase
    {
        [IntegrationTest]
        public void Viewer_By_OAuthToken_Matches_Username()
        {
            var query = new Query().Viewer.Select(user => new
            {
                user.Login,
                user.Email,
                user.IsViewer
            });

            var graphqlUser = Connection.Run(query).Result.FirstOrDefault();

            Assert.NotNull(graphqlUser);
            Assert.True(graphqlUser.IsViewer);

            Assert.Equal(Helper.Username, graphqlUser.Login);
        }

        [IntegrationTest]
        public void Viewer_By_GraphyQL_Matches_Api()
        {
            var gitHubClient = GetV3GitHubClient();

            var apiUser = gitHubClient.User.Current().Result;

            var query = new Query().Viewer.Select(user => new
            {
                AvatarUrl = user.AvatarUrl(null),
                user.Bio,
                user.DatabaseId,
                user.Login,
                user.Email,
                user.Company,
                user.CreatedAt,
                user.Name,
                user.Url,
                user.IsHireable,
                user.Location,
            });

            var graphqlUser = Connection.Run(query).Result.FirstOrDefault();

            Assert.NotNull(graphqlUser);

            Assert.Equal(apiUser.AvatarUrl, graphqlUser.AvatarUrl);
            Assert.Equal(apiUser.Bio, graphqlUser.Bio);
            Assert.Equal(apiUser.Company, graphqlUser.Company);
            Assert.Equal(apiUser.CreatedAt, Helper.ParseUtcLocal(graphqlUser.CreatedAt));
            Assert.Equal(apiUser.Email ?? string.Empty, graphqlUser.Email);
            Assert.Equal(apiUser.Hireable ?? false, graphqlUser.IsHireable);
            Assert.Equal(apiUser.Id, graphqlUser.DatabaseId);
            Assert.Equal(apiUser.Location, graphqlUser.Location);
            Assert.Equal(apiUser.Login, graphqlUser.Login);
            Assert.Equal(apiUser.Name, graphqlUser.Name);
            Assert.Equal(apiUser.HtmlUrl, graphqlUser.Url);

            //TODO: Verify the following fields from the old api are not verifiable
            //Followers
            //Blog
            //Collaborators
            //DiskUsage
            //Following
            //OwnedPrivateRepos
            //Plan
            //PrivateGists
            //PublicGists
            //TotalPrivateRepos
        }
    }
}