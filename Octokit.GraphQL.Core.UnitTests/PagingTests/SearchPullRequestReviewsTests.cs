using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AgileObjects.ReadableExpressions;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class SearchPullRequestReviewsTests
    {
        static SearchPullRequestReviewsTests()
        {
            ExpressionCompiler.IsUnitTesting = true;
        }

        private ICompiledQuery<IEnumerable<List<string>>> TestQuery() =>
            new Query()
                .Search("query")
                .Nodes
                .Select(issueOrPr =>
                    issueOrPr.Switch<List<string>>(when =>
                        when.PullRequest(pr => pr
                            .Commits(null, null, null, null)
                            .AllPages()
                            .Select(x => x.Commit.AbbreviatedOid).ToList())))
                .Compile();

        private SimpleQuery<IEnumerable<List<string>>> TestMasterQuery() => TestQuery().GetMasterQuery();

        private IReadOnlyList<ISubquery> TestQuerySubqueries() => TestQuery().GetSubqueries();

        [Fact]
        public void Creates_MasterQuery()
        {
            var expected = @"query {
  search(query: ""query"") {
    nodes {
      __typename
      ... on PullRequest {
        id
        commits(first: 100) {
          pageInfo {
            hasNextPage
            endCursor
          }
          nodes {
            commit {
              abbreviatedOid
            }
          }
        }
      }
    }
  }
}";

            Assert.Equal(expected, TestMasterQuery().ToString(), ignoreLineEndingDifferences: true);
        }
    }
}