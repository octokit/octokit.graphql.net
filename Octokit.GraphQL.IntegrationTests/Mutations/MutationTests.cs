using System;
using System.Linq;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Octokit.GraphQL.Model;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests.Mutations
{
    public class MutationTests : IntegrationTestBase, IDisposable
    {
        private readonly string _repoName;
        private readonly GitHubClient _gitHubClient;
        private readonly Octokit.Repository _repository;
        private readonly string _ticks;
        private readonly ID _repositoryId;

        public MutationTests()
        {
            _ticks = DateTime.Now.Ticks.ToString();
            _repoName = $"TestProject_{_ticks}";

            _gitHubClient = GetV3GitHubClient();
            _repository = _gitHubClient.Repository.Create(new NewRepository(_repoName)).Result;

            var repositoryQuery = new Query()
                .Repository(owner: Helper.Username, name: _repoName)
                .Select(r => r.Id );

            _repositoryId = Connection.Run(repositoryQuery).Result;
        }

        [IntegrationTest]
        public void Create_And_Delete_Project()
        {
            var projectName = "ProjectName_" + _ticks;
            var projectDesc = "ProjectDesc_" + _ticks;
            var clientMutationId = "abc123";

            var createProjectQuery = new Mutation() 
                .CreateProject(new CreateProjectInput()
                {
                    Name = projectName,
                    OwnerId = _repositoryId,
                    
                    //TODO: This is not required but the code fails if we leave this empty
                    ClientMutationId = clientMutationId,
                    Body = projectDesc
                })
                .Select(payload => new {payload.ClientMutationId, ProjectId = payload.Project.Id, ProjectName = payload.Project.Name, ProjectOwnerId = payload.Project.Owner.Id});

            var projectData = Connection.Run(createProjectQuery).Result;

            Assert.Equal(projectData.ClientMutationId, clientMutationId);
            Assert.Equal(projectData.ProjectName, projectName);
            Assert.Equal(projectData.ProjectOwnerId, _repositoryId);

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

            var deleteResult = Connection.Run(deleteProjectQuery).Result;

            Assert.Equal(deleteResult.ClientMutationId, clientMutationId);
            Assert.Equal(deleteResult.ProjectOwnerId, _repositoryId);

        }

        [IntegrationTest]
        public void Star_And_Unstar_Project()
        {
            var viewerHasStarredQuery = new Query()
                .Repository(owner: Helper.Username, name: _repoName)
                .Select(repository => repository.ViewerHasStarred);

            var viewerHasStarred = Connection.Run(viewerHasStarredQuery).Result;
            Assert.False(viewerHasStarred);

            var clientMutationId = "abc123";

            var addStarQuery = new Mutation().AddStar(new AddStarInput
            {
                ClientMutationId = clientMutationId,
                StarrableId = _repositoryId
            }).Select(payload => new
            {
                payload.ClientMutationId,
                StarrableId = payload.Starrable.Id,
                payload.Starrable.ViewerHasStarred
            });

            var addStarResult = Connection.Run(addStarQuery).Result;

            Assert.Equal(addStarResult.ClientMutationId, clientMutationId);
            Assert.Equal(addStarResult.StarrableId, _repositoryId);
            Assert.True(addStarResult.ViewerHasStarred);

            clientMutationId = "def456";

            var removeStarQuery = new Mutation().RemoveStar(new RemoveStarInput()
            {
                ClientMutationId = clientMutationId,
                StarrableId = _repositoryId
            }).Select(payload => new
            {
                payload.ClientMutationId,
                StarrableId = payload.Starrable.Id,
                payload.Starrable.ViewerHasStarred
            });

            var removeStarResult = Connection.Run(removeStarQuery).Result;

            Assert.Equal(removeStarResult.ClientMutationId, clientMutationId);
            Assert.Equal(removeStarResult.StarrableId, _repositoryId);
            Assert.False(removeStarResult.ViewerHasStarred);

        }

        public void Dispose()
        {
            _gitHubClient.Repository.Delete(_repository.Id).Wait();
        }
    }
}