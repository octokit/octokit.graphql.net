using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class PagingTests
    {
        public class Repository_Issues_AllPages
        {
            ICompiledQuery<IEnumerable<int>> TestQuery { get; } = new Query()
                .Repository("foo", "bar")
                .Issues()
                .AllPages()
                .Select(issue => issue.Number)
                .Compile();

            [Fact]
            public void Creates_MasterQuery()
            {
                var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    issues(first: 100) {
      pageInfo {
        hasNextPage
        endCursor
      }
      nodes {
        number
      }
    }
  }
}";

                var master = TestQuery.GetMasterQuery();

                Assert.Equal(expected, master.ToString());
            }

            [Fact]
            public void Creates_MasterQuery_Expression()
            {
                Expression<Func<JObject, IEnumerable<int>>> expected = data =>
                    Rewritten.List.Select(
                        data["data"]["repository"]["issues"]["nodes"],
                        issue => issue["number"].ToObject<int>());

                var master = TestQuery.GetMasterQuery();

                Assert.Equal(expected.ToString(), master.Expression.ToString());
            }

            [Fact]
            public void Repository_Issues_AllPages_Creates_Subquery()
            {
                var expected = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    ... on Repository {
      __typename
      issues(first: 100, after: $__after) {
        pageInfo {
          hasNextPage
          endCursor
        }
        nodes {
          number
        }
      }
    }
  }
}";

                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);
                Assert.Equal(expected.ToString(), subqueries[0].Query.ToString());
            }

            [Fact]
            public void Repository_Issues_AllPages_Creates_Subquery_PageInfo_Selector()
            {
                Expression<Func<JObject, JToken>> expected = data =>
                        data["data"]["node"]["issues"]["pageInfo"];

                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);
                Assert.Equal(expected.ToString(), subqueries[0].PageInfo.ToString());
            }

            [Fact]
            public void Repository_Issues_AllPages_Creates_Subquery_ParentPageInfo_Selector()
            {
                Expression<Func<JObject, JToken>> expected = data =>
                        data["data"]["repository"]["issues"]["pageInfo"];

                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);
                Assert.Equal(expected.ToString(), subqueries[0].ParentPageInfo.ToString());
            }
        }
    }
}
