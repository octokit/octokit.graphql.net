using System;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.IntegrationTests.Utilities;

namespace Octokit.GraphQL.IntegrationTests.Mutations
{
    public class MutationTests : IntegrationTestBase
    {
        [IntegrationTest]
        public void blah()
        {
            var ticks = DateTime.Now.Ticks.ToString();
            var repoName = $"TestProject_{ticks}";

            var createProjectQuery = new Mutation()
                .CreateProject(new CreateProjectInput() {Name = repoName, OwnerId = Helper.Username})
                .Select(payload => new {payload.Project.Name, payload.Project.Owner});

            var createResult = Connection.Run(createProjectQuery).Result;

            var deleteProjectQuery = new Mutation()
                .DeleteProject(new DeleteProjectInput() {ProjectId = Helper.Username + "\\" + repoName})
                .Select(payload => payload.Owner.Id);

            var deleteResult = Connection.Run(createProjectQuery).Result;
        }
    }
}