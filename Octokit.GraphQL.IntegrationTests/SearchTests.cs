using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Octokit.GraphQL.Model;
using static Octokit.GraphQL.Variable;

namespace Octokit.GraphQL.IntegrationTests
{
    public class SearchTests : IntegrationTestBase
    {
        [IntegrationTest]
        public void Should_Query_Commits()
        {
            var query = new Query()
                .Search("language:JavaScript stars:>10000", SearchType.Issue, first: 3)
                .Nodes
                .Select(item => item.Switch<string>(when =>
                    when.Issue(issue => issue.Id.Value)));

            var queryBuilder = new QueryBuilder();
            var queryString = queryBuilder.Build(query);;

            var vars1 = new Dictionary<string, object>() { { "a", 1 }, { "b", true }, { "c", "foor" } };
            var vars2 = new Dictionary<string, object>() { { "a", 2 }, { "b", false }, { "c", "bar" } };

            //var results = Connection.Run(query).Result.ToArray();
        }

        [IntegrationTest]
        public async Task Should_Query_Issues()
        {
            var filter = $"type:pr repo:github/VisualStudio";

            var query = new Query().Search(Var(nameof(filter)), first: 2, type: SearchType.Issue)
                .Select(page => new 
                {
                    Items = page.Nodes
                        .Select(issueOrPr => new
                        {
                            Result = issueOrPr.Switch<PullRequestListItem>(when =>
                                when.PullRequest(pr => new PullRequestListItem
                                {
                                    Number = pr.Number,
                                    Reviews = pr.Reviews(null, null, null, null, null, null)
                                        .AllPages()
                                        .Select(review => new Review
                                        {
                                            Body = review.Body,
                                            CommentCount = review
                                                .Comments(null, null, null, null)
                                                .TotalCount,
                                        }).ToList(),
                                }))
                        }).ToList()
                }).Compile();

            var vars = new Dictionary<string, object>
            {
                { nameof(filter), filter }
            };

            var result = await Connection.Run(query, vars);
        }

        [IntegrationTest]
        public async Task Should_Query_Issues2()
        {
            var filter = $"type:pr repo:github/VisualStudio";

            var query = new Query().Search(query: Var(nameof(filter)), SearchType.Issue, 100)
                .AllPages()
                .Select(item => new IssueishItemWrapper
                {
                    SuggestionItem = item
                        .Switch<IssueishItem>(selector => selector
                            .Issue(issue => new IssueishItem { Number = issue.Number })
                            .PullRequest(pullRequest => new IssueishItem { Number = pullRequest.Number }))
                }).Compile();

            var vars = new Dictionary<string, object>
            {
                { nameof(filter), filter }
            };

            var result = await Connection.Run(query, vars);
        }

        public class IssueishItemWrapper
        {
            public IssueishItem SuggestionItem { get; set; }
        }

        public class IssueishItem
        {
            public int Number { get; set; }
        }

        public class PullRequestListItem
        {
            public int Number { get; set; }
            public IList<Review> Reviews { get; set; }
        }

        public class Review
        {
            public string Body { get; set; }
            public int CommentCount { get; set; }
            public int Count => CommentCount + (!string.IsNullOrWhiteSpace(Body) ? 1 : 0);
        }
    }
}