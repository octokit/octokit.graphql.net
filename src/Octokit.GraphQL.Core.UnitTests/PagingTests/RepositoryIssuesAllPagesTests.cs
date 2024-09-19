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
    internal class SubqueryPlaceholder
    {
        public static readonly ISubquery placeholder;
    }

    public class Repository_Issues_AllPages
    {
        static Repository_Issues_AllPages()
        {
            ExpressionCompiler.IsUnitTesting = true;
        }

        private ICompiledQuery<IEnumerable<int>> TestQuery(int? allPages = null) =>
            allPages.HasValue ? 
                new Query()
                    .Repository("foo", "bar")
                    .Issues()
                    .AllPages(allPages.Value)
                    .Select(issue => issue.Number)
                    .Compile() :
                new Query()
                    .Repository("foo", "bar")
                    .Issues()
                    .AllPages()
                    .Select(issue => issue.Number)
                    .Compile();

        private SimpleQuery<IEnumerable<int>> TestMasterQuery(int? allPages = null) => TestQuery(allPages).GetMasterQuery();

        private IReadOnlyList<ISubquery> TestQuerySubqueries(int? allPages = null) => TestQuery(allPages).GetSubqueries();

        private SimpleSubquery<IEnumerable<int>> TestQueryFirstSubquery(int? allPages = null) => (SimpleSubquery<IEnumerable<int>>)TestQuerySubqueries(allPages).First();

        [Fact]
        public void Creates_MasterQuery()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    id
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

            Assert.Equal(expected, TestMasterQuery().ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Creates_MasterQuery_CustomPageSize()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    id
    issues(first: 10) {
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

            Assert.Equal(expected, TestMasterQuery(10).ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Creates_MasterQuery_Expression()
        {
            Expression<Func<JObject, IEnumerable<int>>> expected = data =>
                (IEnumerable<int>)Rewritten.List.ToSubqueryList(
                    Rewritten.List.Select(
                        data["data"]["repository"]["issues"]["nodes"],
                        issue => issue["number"].ToObject<int>()),
                    data.Annotation<ISubqueryRunner>(), SubqueryPlaceholder.placeholder);

            ExpressionRewriterAssertions.AssertCompiledQueryExpressionEqual(expected, TestMasterQuery(10), "SimpleSubquery<IEnumerable<int>>");
        }

        [Fact]
        public void Creates_Subquery()
        {
            var expected = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    __typename
    ... on Repository {
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

            Assert.Single(TestQuerySubqueries());
            Assert.Equal(expected, TestQueryFirstSubquery().ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Creates_Subquery_CustomPageSize()
        {
            var expected = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    __typename
    ... on Repository {
      issues(first: 10, after: $__after) {
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

            Assert.Single(TestQuerySubqueries(10));
            Assert.Equal(expected, TestQueryFirstSubquery(10).ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Creates_Subquery_Expression()
        {
            Expression<Func<JObject, IEnumerable<int>>> expected =
                data => (IEnumerable<int>)Rewritten.List.Select(
                    Rewritten.Interface.Cast(data["data"]["node"], "Repository")["issues"]["nodes"],
                    issue => issue["number"].ToObject<int>()).ToList();
            var expectedString = expected.ToReadableString();

            var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery().ResultBuilder);
            var actualString = actual.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_Subquery_Expression_PageInfo()
        {
            Expression<Func<JObject, JToken>> expected = data => data.SelectToken("data.node.issues.pageInfo");
            var expectedString = expected.ToReadableString();

            var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery().PageInfo);
            var actualString = actual.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_Subquery_Expression_ParentId()
        {
            Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.repository.id");
            var expectedString = expected.ToReadableString();

            var actualExpression = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery().ParentIds);
            var actualString = actualExpression.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_Subquery_Expression_ParentPage()
        {
            Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.repository.issues.pageInfo");
            var expectedString = expected.ToReadableString();

            var actualExpression = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery().ParentPageInfo);
            var actualString = actualExpression.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public async Task Reads_All_Pages()
        {
            int page = 0;

            string Execute(string _, IDictionary<string, string> variables)
            {
                switch (page++)
                {
                    case 0:
                        Assert.Null(variables);
                        return @"{
  data: {
    ""repository"": {
      ""id"": ""repoid"",
      ""issues"": {
        ""pageInfo"": {
          ""hasNextPage"": true,
          ""endCursor"": ""end0""
        },
        ""nodes"": [
          { ""number"": 0 },
          { ""number"": 1 },
          { ""number"": 2 },
        ]
      }
    }
  }
}";
                    case 1:
                        Assert.NotNull(variables);
                        Assert.Equal(variables["__id"], "repoid");
                        Assert.Equal(variables["__after"], "end0");
                        return @"{
  data: {
    ""node"": {
      ""__typename"": ""Repository"",
      ""issues"": {
        ""pageInfo"": {
          ""hasNextPage"": true,
          ""endCursor"": ""end1""
        },
        ""nodes"": [
          { ""number"": 3 },
          { ""number"": 4 },
        ]
      }
    }
  }
}";
                    case 2:
                        Assert.NotNull(variables);
                        Assert.Equal(variables["__after"], "end1");
                        return @"{
  data: {
    ""node"": {
      ""__typename"": ""Repository"",
      ""issues"": {
        ""pageInfo"": {
          ""hasNextPage"": false,
          ""endCursor"": ""end2""
        },
        ""nodes"": [
          { ""number"": 5 },
        ]
      }
    }
  }
}";
                    default:
                        throw new NotSupportedException("Should not get here");
                }
            }

            var connection = new MockConnection(Execute);
            var result = (await connection.Run(TestQuery())).ToList();

            Assert.Equal(Enumerable.Range(0, 6).ToList(), result);
        }
    }
}