using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Octokit.GraphQL.Model;
using Xunit;
using static Octokit.GraphQL.Variable;

namespace Octokit.GraphQL.IntegrationTests.Queries
{
    public class PullRequestTests : IntegrationTestBase
    {
        [IntegrationTest]
        public async Task Should_Query_Commits()
        {
            var query = new Query()
                .Repository(owner: "octokit", name: "octokit.net")
                .PullRequest(number: 1)
                .Commits(first: 3)
                .Nodes
                .Select(pullRequestCommit => new
                {
                    pullRequestCommit.Commit.Id,
                    pullRequestCommit.Commit.Message,
                    pullRequestCommit.Commit.Author.Name
                });

            var results = (await Connection.Run(query)).ToArray();

            Assert.Single(results);
            Assert.Equal("MDY6Q29tbWl0NzUyODY3OTpkYWZhYjhhZjA0ODM5NDU1ODM4Y2QzZmRlMTFkMTM5MTc0MTYyZmFh", results[0].Id.Value);
            var expectedMessage = "Adding README, CONTRIBUTING, LICENSE\n\nWe plan to release this code under the MIT license so might as well get\nthe right things in place early.";
            Assert.Equal(expectedMessage, results[0].Message);
            Assert.Equal("Haacked", results[0].Name);
        }

        [IntegrationTest]
        public async Task Should_Query_Title_With_Vars()
        {
            var query = new Query()
                .Repository(owner: Var("owner"), name: Var("name"))
                .PullRequest(number: Var("number"))
                .Select(x => x.Title)
                .Compile();

            var vars = new Dictionary<string, object>
            {
                { "owner", "octokit" },
                { "name", "octokit.net" },
                { "number", 1 },
            };

            var result = await Connection.Run(query, vars);

            Assert.Equal("Adding README, CONTRIBUTING, LICENSE", result);
        }

        [IntegrationTest]
        public async Task Can_Use_Conditional_In_BaseRef_Query()
        {
            var query = new Query()
                .Repository(owner: "octokit", name: "octokit.net")
                .PullRequest(number: 1)
                .Select(pr => pr.BaseRef != null ? pr.BaseRef.Name : null);

            var result = await Connection.Run(query);

            Assert.Equal("master", result);
        }

        [IntegrationTest]
        public async Task Can_Use_Conditional_When_Selecting_Base_Repository_Owner()
        {
            var query = new Query()
                .Repository(owner: "octokit", name: "octokit.net")
                .PullRequest(number: 1)
                .Select(pr => pr.BaseRef != null ? pr.BaseRef.Repository.Owner.Login : null);

            var result = await Connection.Run(query);

            Assert.Equal("octokit", result);
        }

        [IntegrationTest]
        public async Task Can_AutoPage_Reviews_Comments()
        {
            // nodegit/nodegit#1331 has a review with >100 comments.
            var query = new Query()
                .Repository(owner: "nodegit", name: "nodegit")
                .PullRequest(number: 1331)
                .Select(pr => new
                {
                    pr.Title,
                    Reviews = pr.Reviews(null, null, null, null, null, null).AllPages().Select(review => new
                    {
                        Comments = review.Comments(null, null, null, null).AllPages().Select(comment => new
                        {
                            comment.Body,
                        }).ToList(),
                    }).ToList(),
                }).Compile();

            var result = await Connection.Run(query);

            Assert.True(result.Reviews.Count > 0);
            Assert.True(result.Reviews[0].Comments.Count > 100);
        }

        [IntegrationTest]
        public async Task Can_AutoPage_Reviews_Comments_2()
        {
            // Microsoft/MixedRealityToolkit-Unity#1884 has >100 reviews, one of which has >100 comments.
            var query = new Query()
                .Repository(owner: "Microsoft", name: "MixedRealityToolkit-Unity")
                .PullRequest(number: 1884)
                .Select(pr => new
                {
                    pr.Title,
                    Reviews = pr.Reviews(null, null, null, null, null, null).AllPages().Select(review => new
                    {
                        Comments = review.Comments(null, null, null, null).AllPages().Select(comment => new
                        {
                            comment.Id,
                            comment.Body,
                        }).ToList(),
                    }).ToList(),
                }).Compile();

            var result = await Connection.Run(query);

            Assert.True(result.Reviews.Count > 100);
            Assert.Contains(result.Reviews, x => x.Comments.Count > 100);
        }

        [IntegrationTest]
        public async Task PullRequest_Status_Context_CanBeNull()
        {
            var query = new Query()
            .Repository(owner: "StanleyGoldman", name: "ConsoleCoreAppWithYamlForNoReason")
            .PullRequests(1)
            .Select(page => 
                page.Nodes.Select(pr => new 
                {
                    Id = pr.Id.Value,
                    LastCommit = pr.Commits(null, null, 1, null).Nodes.Select(commit => new
                    {
                        commit.Id,
                        Statuses = commit.Commit.Status
                            .Select(status => status.Contexts.Select(context => context.State).ToList())
                            .SingleOrDefault()
                    }).ToList().FirstOrDefault(),
                }).ToList().First()
            ).Compile();

            var result = await Connection.Run(query);
            Assert.Null(result.LastCommit.Statuses);
        }
    }
}