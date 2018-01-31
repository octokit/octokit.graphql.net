using System.Collections.Generic;
using System.Linq;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Octokit.GraphQL.Model;
using Xunit;
using static Octokit.GraphQL.Variable;

namespace Octokit.GraphQL.IntegrationTests.Queries
{
    public class IssueTests : IntegrationTestBase
    {
        [IntegrationTest]
        public void Should_Query_Issues_By_Repository()
        {
            var query = new GraphQL.Query().Repository("octokit", "octokit.net").Issues(first: 3).Nodes.Select(i => new
            {
                i.Title,
                RepositoryName = i.Repository.Name,
            });

            var results = Connection.Run(query).Result;
            foreach (var result in results)
            {
                Assert.Equal("octokit.net", result.RepositoryName);
            }
        }

        [IntegrationTest]
        public void Should_Query_Issues_By_State_And_Repository()
        {
            var openState = new[] { IssueState.Closed };
            var query = new GraphQL.Query().Repository("octokit", "octokit.net").Issues(first: 3, states: openState).Nodes.Select(i => new
            {
                i.Title,
                i.State,
                RepositoryName = i.Repository.Name,
            });

            var results = Connection.Run(query).Result.ToArray();
            foreach (var result in results)
            {
                Assert.Equal("octokit.net", result.RepositoryName);
                Assert.Equal(IssueState.Closed, result.State);
            }
        }

        [IntegrationTest]
        public void Should_Query_Issues_With_Variable()
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
            var results = Connection.Run(compiled, vars).Result.ToArray();
            Assert.Equal(3, results.Length);
        }

        [IntegrationTest(Skip = "Querying unions like this no longer works")]
        public void Should_Query_Union_Issue_Or_PullRequest2()
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

            Assert.NotNull(Connection.Run(query).Result.Last().ClosedEventId);
        }
    }
}