using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests.Queries
{
    public class ViewerTests: IntegrationTestBase
    {
        [IntegrationTest]
        public async Task Viewer_By_OAuthToken_Matches_Username()
        {
            var query = new GraphQL.Query().Viewer.Select(user => new
            {
                user.Login,
                user.Email,
                user.IsViewer
            });

            var graphqlUser = await Connection.Run(query);

            Assert.NotNull(graphqlUser);
            Assert.True(graphqlUser.IsViewer);

            Assert.Equal(Helper.Username, graphqlUser.Login);
        }

        [IntegrationTest]
        public async Task Viewer_By_GraphyQL_Matches_Api()
        {
            var gitHubClient = GetV3GitHubClient();

            var apiUser = await gitHubClient.User.Current();

            var query = new GraphQL.Query().Viewer.Select(user => new
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

            var graphqlUser = await Connection.Run(query);

            Assert.NotNull(graphqlUser);

            Assert.Equal(apiUser.AvatarUrl.Split("?").First(), graphqlUser.AvatarUrl.Split("?").First());
            Assert.Equal(apiUser.Bio ?? string.Empty, graphqlUser.Bio);
            Assert.Equal(apiUser.Company, graphqlUser.Company);

            Assert.Equal(apiUser.CreatedAt.ToUniversalTime(), graphqlUser.CreatedAt.ToUniversalTime());

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

        [IntegrationTest]
        public async Task Viewer_Has_No_OrganizationVerifiedDomainEmails_For_Octokit()
        {
            var query = new GraphQL.Query().Viewer.Select(user => user.OrganizationVerifiedDomainEmails("octokit").ToList());

            var emails = await Connection.Run(query);

            Assert.Empty(emails);
        }

        [IntegrationTest(Skip = "This is a fragile integration test and we need to validate if it is still useful/correct. It is currently failing")]
        public async Task DateTime_Filter_Works()
        {
            var query = new GraphQL.Query()
                        .Viewer.ContributionsCollection(from: Variable.Var("start"))
                        .Select(c => c.User.Name);

            var vars = new Dictionary<string, object>
            {
                { "start", new DateTimeOffset(2000, 1, 2, 3, 4, 5, default) }
            };

            var response = await Connection.Run(query.Compile(), vars);

            // no server error
            Assert.NotNull(response);
        }
    }
}
