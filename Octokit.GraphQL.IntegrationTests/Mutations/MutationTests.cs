using System;
using System.Linq;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests.Mutations
{
    public class MutationTests : IntegrationTestBase, IDisposable
    {
        private readonly string _repoName;
        private readonly GitHubClient _gitHubClient;
        private readonly Octokit.Repository _repository;
        private readonly string _ticks;

        public MutationTests()
        {
            _ticks = DateTime.Now.Ticks.ToString();
            _repoName = $"TestProject_{_ticks}";

            _gitHubClient = GetV3GitHubClient();
            _repository = _gitHubClient.Repository.Create(new NewRepository(_repoName)).Result;
        }

        [IntegrationTest]
        public void Create_And_Delete_Project()
        {
            var repositoryQuery = new Query().Repository(Helper.Username, _repoName)
                .Select(r => new {r.Name, r.Id});

            var repository = Connection.Run(repositoryQuery).Result.First();

            var projectName = "ProjectName_" + _ticks;
            var projectDesc = "ProjectDesc_" + _ticks;
            var clientMutationId = "abc123";

            var createProjectQuery = new Mutation() 
                .CreateProject(new CreateProjectInput()
                {
                    Name = projectName,
                    OwnerId = repository.Id,
                    
                    //TODO: This is not required but the code fails if we leave this empty
                    ClientMutationId = clientMutationId,
                    Body = projectDesc
                })
                .Select(payload => new {payload.ClientMutationId, ProjectId = payload.Project.Id, ProjectName = payload.Project.Name, ProjectOwnerId = payload.Project.Owner.Id});

            var projectData = Connection.Run(createProjectQuery).Result.First();

            Assert.Equal(projectData.ClientMutationId, clientMutationId);
            Assert.Equal(projectData.ProjectName, projectName);
            Assert.Equal(projectData.ProjectOwnerId, repository.Id);

            clientMutationId = "def456";

            var deleteProjectQuery = new Mutation()
                .DeleteProject(new DeleteProjectInput()
                {
                    ProjectId = projectData.ProjectId,

                    //TODO: This is not required but the code fails if we leave this empty
                    ClientMutationId = clientMutationId

                })
                .Select(payload => new
                {
                    ProjectOwnerId = payload.Owner.Id,
                    payload.ClientMutationId,
                });

            var deleteResult = Connection.Run(deleteProjectQuery).Result.First();

            Assert.Equal(deleteResult.ClientMutationId, clientMutationId);
            Assert.Equal(deleteResult.ProjectOwnerId, repository.Id);

        }

        public void Dispose()
        {
            _gitHubClient.Repository.Delete(_repository.Id).Wait();
        }
    }
}