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
        private readonly ID _ownerId;
        private readonly ID _repositoryId;

        public MutationTests()
        {
            _ticks = DateTime.Now.Ticks.ToString();
            _repoName = $"TestProject_{_ticks}";

            _gitHubClient = GetV3GitHubClient();
            _repository = _gitHubClient.Repository.Create(new NewRepository(_repoName)).Result;

            var repositoryQuery = new Query()
                .Repository(owner: Helper.Username, name: _repoName)
                .Select(r => new
                {
                    RepositoryId = r.Id,
                    OwnerId = r.Owner.Id,
                });

            var repositoryData = Connection.Run(repositoryQuery).Result;
            _repositoryId = repositoryData.RepositoryId;
            _ownerId = repositoryData.OwnerId;
        }

        [IntegrationTest]
        public void Create_And_Delete_ProjectV2()
        {
            var projectName = "ProjectName_" + _ticks;
            var clientMutationId = "abc123";

            var createProjectQuery = new Mutation() 
                .CreateProjectV2(new CreateProjectV2Input()
                {
                    Title = projectName,
                    OwnerId = _ownerId,
                    RepositoryId = _repositoryId,
                    ClientMutationId = clientMutationId,
                })
                .Select(payload => new
                {
                    payload.ClientMutationId,
                    ProjectId = payload.ProjectV2.Id,
                    ProjectName = payload.ProjectV2.Title,
                    ProjectOwnerId = payload.ProjectV2.Owner.Id,
                });

            var projectData = Connection.Run(createProjectQuery).Result;

            Assert.Equal(projectData.ClientMutationId, clientMutationId);
            Assert.Equal(projectData.ProjectName, projectName);
            Assert.Equal(projectData.ProjectOwnerId, _ownerId);

            clientMutationId = "def456";

            var deleteProjectQuery = new Mutation()
                .DeleteProjectV2(new DeleteProjectV2Input()
                {
                    ProjectId = projectData.ProjectId,
                    ClientMutationId = clientMutationId
                })
                .Select(payload => new
                {
                    payload.ClientMutationId,
                    ProjectId = payload.ProjectV2.Id,
                });

            var deleteResult = Connection.Run(deleteProjectQuery).Result;

            Assert.Equal(deleteResult.ClientMutationId, clientMutationId);
            Assert.Equal(deleteResult.ProjectId, projectData.ProjectId);

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