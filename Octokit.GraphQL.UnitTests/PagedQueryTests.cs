using System;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;
using Xunit;
using static Octokit.GraphQL.Variable;

namespace Octokit.GraphQL.UnitTests
{
    public class PagedQueryTests
    {
        [Fact]
        public void Rewrites_AllPages_Of_PullRequest_Titles()
        {
            // A simple query to select all pages of all pull request titles.
            var query = new Query()
                .Repository("octokit", "octokit.net")
                .PullRequests().AllPages()
                .Select(x => x.Title);

            // Should be rewritten to this paged query.
            var rewritten = new Query()
                .Repository("octokit", "octokit.net")
                .PullRequests(Var("first"), Var("after"))
                .Select(connection => new Page<string>
                {
                    HasNextPage = connection.PageInfo.HasNextPage,
                    EndCursor = connection.PageInfo.EndCursor,
                    Items = connection.Nodes.Select(x => x.Title).ToList(),
                });

            var result = new PagedQueryBuilder().RewriteExpression(query.Expression);
            var expected = ExpectedExpression.Normalize(rewritten.Expression);
            var actual = ExpectedExpression.Normalize(result);

            Assert.Equal(expected.ToString(), actual.ToString());
        }

        [Fact]
        public void Generates_Master_Query_For_Repository_Name_And_AllPages_Of_PullRequest_Title()
        {
            // This query selects a single repository name and all pages of the repository's pull
            // requests as an inner collection.
            var query = new Query()
                .Repository("octokit", "octokit.net")
                .Select(x => new
                {
                    x.Name,
                    PullRequests = x.PullRequests(null, null, null, null, null, null, null, null, null)
                        .AllPages()
                        .Select(y => y.Title).ToList(),
                }).Compile();

            // This should produce a "master" query as follows. There's no C# expression that represents
            // this as it's rewritten in-place but we can check the generated GraphQL query.
            var expected = @"query {
  repository(owner: ""octokit"", name: ""octokit.net"") {
    name
    pullRequests(first: 100) {
      pageInfo {
        hasNextPage
        endCursor
      }
      nodes {
        title
      }
    }
  }
}";

            Assert.Equal(expected, query.ToString());
        }
    }
}
