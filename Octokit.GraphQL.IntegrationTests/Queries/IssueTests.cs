using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Octokit.GraphQL.Model;
using Xunit;
using static Octokit.GraphQL.Variable;

namespace Octokit.GraphQL.IntegrationTests.Queries
{
    public class IssueTests : IntegrationTestBase
    {
        [IntegrationTest]
        public async Task Should_Query_Issues_By_Repository()
        {
            var query = new GraphQL.Query().Repository("octokit", "octokit.net").Issues(first: 3).Nodes.Select(i => new
            {
                i.Title,
                RepositoryName = i.Repository.Name,
            });

            var results = await Connection.Run(query);
            foreach (var result in results)
            {
                Assert.Equal("octokit.net", result.RepositoryName);
            }
        }

        [IntegrationTest]
        public async Task Should_Query_Issues_By_State_And_Repository()
        {
            var openState = new[] { IssueState.Closed };
            var query = new GraphQL.Query().Repository("octokit", "octokit.net").Issues(first: 3, states: openState).Nodes.Select(i => new
            {
                i.Title,
                i.State,
                RepositoryName = i.Repository.Name,
            });

            var results = (await Connection.Run(query)).ToArray();
            foreach (var result in results)
            {
                Assert.Equal("octokit.net", result.RepositoryName);
                Assert.Equal(IssueState.Closed, result.State);
            }
        }

        [IntegrationTest]
        public async Task Should_Query_Issues_With_Variable()
        {
            var openState = new[] { IssueState.Closed };
            var query = new Query()
                .Repository("octokit", "octokit.net")
                .Issues(Var("first"), states: openState)
                .Nodes
                .Select(i => new
            {
                i.Title,
                i.State,
                RepositoryName = i.Repository.Name,
            });

            var vars = new Dictionary<string, object>
            {
                { "first", 3 }
            };

            var compiled = query.Compile();
            var results = (await Connection.Run(query)).ToArray();
            Assert.Equal(3, results.Length);
        }

        [IntegrationTest]
        public async Task Should_Query_Issue_Page_With_Author_Model()
        {
            var openState = new[] { IssueState.Closed };
            var query = new Query()
                .Repository("octokit", "octokit.net")
                .Issues(first: 100, after: Var("after"))
                .Select(connection => new
                {
                    connection.PageInfo.EndCursor,
                    connection.PageInfo.HasNextPage,
                    connection.TotalCount,
                    Items = connection.Nodes.Select(issue => new
                    {
                        NodeId = issue.Id,
                        Number = issue.Number,
                        Title = issue.Title,
                        Author = new ActorModel
                        {
                            AvatarUrl = issue.Author.AvatarUrl(null),
                            Login = issue.Author.Login,
                        },
                        UpdatedAt = issue.UpdatedAt,
                    }).ToList(),
                }).Compile();

            var vars = new Dictionary<string, object>
            {
                { "after", null }
            };

            var results = await Connection.Run(query, vars);

            Assert.Equal(100, results.Items.Count);
        }

        [IntegrationTest]
        public async Task Can_Manually_Page_Issue_Comments_By_Node_Id()
        {
            var masterQuery = new Query()
                .Repository("octokit", "octokit.net")
                .Issue(405)
                .Select(issue => new
                {
                    issue.Id,
                    Comments = issue.Comments(100, null, null, null).Select(page => new
                    {
                        page.PageInfo.HasNextPage,
                        page.PageInfo.EndCursor,
                        Items = page.Nodes.Select(comment => comment.Body).ToList(),
                    }).Single(),
                });

            var pageQuery = new Query()
                .Node(Var("issueId"))
                .Cast<Model.Issue>()
                .Comments(100, Var("after"))
                .Select(page => new
                {
                    page.PageInfo.HasNextPage,
                    page.PageInfo.EndCursor,
                    Items = page.Nodes.Select(comment => comment.Body).ToList(),
                }).Compile();

            var result = await Connection.Run(masterQuery);

            Assert.True(result.Comments.HasNextPage);

            var vars = new Dictionary<string, object>
            {
                { "issueId", result.Id },
                { "after", result.Comments.EndCursor },
            };

            do
            {

                var pageResults = await Connection.Run(pageQuery, vars);
                result.Comments.Items.AddRange(pageResults.Items);
                vars["after"] = pageResults.HasNextPage ? pageResults.EndCursor : null;
            } while (vars["after"] != null);

            Assert.True(result.Comments.Items.Count > 100);
        }

        [IntegrationTest]
        public async Task Can_AutoPage_Issue_Comments()
        {
            var query = new Query()
                .Repository("octokit", "octokit.net")
                .Issue(405)
                .Select(issue => new
                {
                    issue.Id,
                    Comments = issue.Comments(null, null, null, null).AllPages().Select(comment => comment.Body).ToList(),
                });

            var result = await Connection.Run(query);

            Assert.True(result.Comments.Count > 100);
        }

        [IntegrationTest]
        public async Task Can_AutoPage_Issues_Comments()
        {
            var query = new Query()
                .Repository("octokit", "octokit.net")
                .Issues().AllPages()
                .Select(issue => new
                {
                    issue.Id,
                    Comments = issue.Comments(null, null, null, null).AllPages().Select(comment => comment.Body).ToList(),
                });

            var result = (await Connection.Run(query)).ToList();

            Assert.True(result.Count > 100);
            Assert.True(result.Any(x => x.Comments.Count > 100));
        }

        [IntegrationTest(Skip = "Querying unions like this no longer works")]
        public async Task Should_Query_Union_Issue_Or_PullRequest2()
        {
            var query = new Query().Repository("octokit", "octokit.net").Issue(23)
                .Timeline(first: 30).Nodes
                .Select(issueTimelineItem => new
                {
                    AssignedEventId = issueTimelineItem.AssignedEvent.Id,
                    ClosedEventId = issueTimelineItem.ClosedEvent.Id,
                    DemilestonedEventId = issueTimelineItem.DemilestonedEvent.Id,
                    LabeledEventId = issueTimelineItem.LabeledEvent.Id,
                    LockedEventId = issueTimelineItem.LockedEvent.Id,
                    MilestonedEventId = issueTimelineItem.MilestonedEvent.Id,
                    ReferencedEventId = issueTimelineItem.ReferencedEvent.Id,
                    RenamedTitleEventId = issueTimelineItem.RenamedTitleEvent.Id,
                    ReopenedEventId = issueTimelineItem.ReopenedEvent.Id,
                    SubscribedEventId = issueTimelineItem.SubscribedEvent.Id,
                    UnassignedEventId = issueTimelineItem.UnassignedEvent.Id,
                    UnlabeledEventId = issueTimelineItem.UnlabeledEvent.Id,
                    UnlockedEventId = issueTimelineItem.UnlockedEvent.Id,
                    UnsubscribedEventId = issueTimelineItem.UnsubscribedEvent.Id,
                });

            var result = (await Connection.Run(query)).Last();

            Assert.NotNull(result.ClosedEventId);
        }

        class ActorModel
        {
            public string Login { get; set; }
            public string AvatarUrl { get; set; }
        }
    }
}