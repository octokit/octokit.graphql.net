using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AgileObjects.ReadableExpressions;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.Syntax;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class PagingTests
    {
        static readonly ISubquery subqueryPlaceholder;

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
                        data.Annotation<ISubqueryRunner>(), subqueryPlaceholder);

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

                string Execute(string query, IDictionary<string, string> variables)
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

        public class Repository_Name_Issues_AllPages
        {
            static Repository_Name_Issues_AllPages()
            {
                ExpressionCompiler.IsUnitTesting = true;
            }

            private ICompiledQuery<RepositoryModel> TestQuery => new Query()
                .Repository("foo", "bar")
                .Select(repository => new RepositoryModel
                {
                    Name = repository.Name,
                    Issues = repository.Issues(null, null, null, null, new[] { "bug" }).AllPages().Select(issue => new IssueModel
                    {
                        Number = issue.Number,
                    }).ToList()
                }).Compile();

            private SimpleQuery<RepositoryModel> TestMasterQuery => TestQuery.GetMasterQuery();

            private IReadOnlyList<ISubquery> TestQuerySubqueries => TestQuery.GetSubqueries();

            private SimpleSubquery<IEnumerable<IssueModel>> TestQueryFirstSubquery => (SimpleSubquery<IEnumerable<IssueModel>>)TestQuerySubqueries.First();

            [Fact]
            public void Creates_MasterQuery()
            {
                var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    id
    name
    issues(labels: [""bug""], first: 100) {
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

                Assert.Equal(expected, TestMasterQuery.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_MasterQuery_Expression()
            {
                Expression<Func<JObject, RepositoryModel>> expected = data =>
                    Rewritten.Value.Select(
                        data["data"]["repository"],
                        repository => new RepositoryModel
                        {
                            Name = repository["name"].ToObject<string>(),
                            Issues = Rewritten.List.ToSubqueryList(
                                Rewritten.List.Select(
                                    repository["issues"]["nodes"],
                                    issue => new IssueModel
                                    {
                                        Number = issue["number"].ToObject<int>(),
                                    }),
                                data.Annotation<ISubqueryRunner>(),
                                subqueryPlaceholder),
                        });

                ExpressionRewriterAssertions.AssertCompiledQueryExpressionEqual(expected, TestMasterQuery,
                    "SimpleSubquery<IEnumerable<PagingTests.IssueModel>>");
            }

            [Fact]
            public void Creates_Subquery()
            {
                var expected = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    __typename
    ... on Repository {
      issues(first: 100, after: $__after, labels: [""bug""]) {
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

                Assert.Single(TestQuerySubqueries);
                Assert.Equal(expected, TestQueryFirstSubquery.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_Subquery_Expression()
            {
                Expression<Func<JObject, IEnumerable<IssueModel>>> expected =
                    data => (IEnumerable<IssueModel>)Rewritten.List.Select(
                        Rewritten.Interface.Cast(data["data"]["node"], "Repository")["issues"]["nodes"],
                        issue => new IssueModel
                        {
                            Number = issue["number"].ToObject<int>()
                        }).ToList();
                var expectedString = expected.ToReadableString();

                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery.ResultBuilder);
                var actualString = actual.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_Subquery_Expression_PageInfo()
            {
                Expression<Func<JObject, JToken>> expected = data => data.SelectToken("data.node.issues.pageInfo");
                var expectedString = expected.ToReadableString();

                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery.PageInfo);
                var actualString = actual.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_Subquery_Expression_ParentId()
            {
                Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.repository.id");
                var expectedString = expected.ToReadableString();

                var actualExpression = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery.ParentIds);
                var actualString = actualExpression.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_Subquery_Expression_ParentPage()
            {
                Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.repository.issues.pageInfo");
                var expectedString = expected.ToReadableString();

                var actualExpression = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery.ParentPageInfo);
                var actualString = actualExpression.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public async Task Reads_All_Pages()
            {
                int page = 0;

                string Execute(string query, IDictionary<string, string> variables)
                {
                    switch (page++)
                    {
                        case 0:
                            Assert.Null(variables);
                            return @"{
  data: {
    ""repository"": {
      ""id"": ""repoid"",
      ""name"": ""foo"",
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
          ""hasNextPage"": false,
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
                        default:
                            throw new NotSupportedException("Should not get here");
                    }
                }

                var connection = new MockConnection(Execute);
                var result = await connection.Run(TestQuery);

                Assert.Equal(
                    Enumerable.Range(0, 5).ToList(),
                    result.Issues.Select(x => x.Number).ToList());
            }
        }

        public class Repository_Name_Issues_AllPages_To_Dictionary
        {
            static Repository_Name_Issues_AllPages_To_Dictionary()
            {
                ExpressionCompiler.IsUnitTesting = true;
            }

            private ICompiledQuery<RepositoryModelWithDictionary> TestQuery => new Query()
                .Repository("foo", "bar")
                .Select(repository => new RepositoryModelWithDictionary
                {
                    Name = repository.Name,
                    Issues = repository.Issues(null, null, null, null, new[] { "bug" }).AllPages().Select(issue => new IssueModel
                    {
                        Number = issue.Number,
                    }).ToDictionary(x => x.Number, x => x)
                }).Compile();

            private SimpleQuery<RepositoryModelWithDictionary> TestMasterQuery => TestQuery.GetMasterQuery();

            private IReadOnlyList<ISubquery> TestQuerySubqueries => TestQuery.GetSubqueries();

            private SimpleSubquery<IEnumerable<IssueModel>> TestQueryFirstSubquery => (SimpleSubquery<IEnumerable<IssueModel>>)TestQuerySubqueries.First();

            [Fact]
            public void Creates_MasterQuery()
            {
                var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    id
    name
    issues(labels: [""bug""], first: 100) {
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

                Assert.Equal(expected, TestMasterQuery.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_MasterQuery_Expression()
            {
                Expression<Func<JObject, RepositoryModelWithDictionary>> expected = data =>
                    Rewritten.Value.Select(
                        data["data"]["repository"],
                        repository => new RepositoryModelWithDictionary
                        {
                            Name = repository["name"].ToObject<string>(),
                            Issues = (IDictionary<int, PagingTests.IssueModel>)Rewritten.List.ToSubqueryDictionary(
                                Rewritten.List.Select(
                                    repository["issues"]["nodes"],
                                    issue => new PagingTests.IssueModel
                                    {
                                        Number = issue["number"].ToObject<int>()
                                    }),
                                data.Annotation<ISubqueryRunner>(),
                                subqueryPlaceholder,
                                x => x.Number,
                                x => x)
                        });

                ExpressionRewriterAssertions.AssertCompiledQueryExpressionEqual(expected, TestMasterQuery,
                    "SimpleSubquery<IEnumerable<PagingTests.IssueModel>>");
            }

            [Fact]
            public void Creates_Subquery()
            {
                var expected = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    __typename
    ... on Repository {
      issues(first: 100, after: $__after, labels: [""bug""]) {
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

                Assert.Single(TestQuerySubqueries);
                Assert.Equal(expected, TestQueryFirstSubquery.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_Subquery_Expression()
            {
                Expression<Func<JObject, IEnumerable<IssueModel>>> expected =
                    data => (IEnumerable<IssueModel>)Rewritten.List.Select(
                        Rewritten.Interface.Cast(data["data"]["node"], "Repository")["issues"]["nodes"],
                        issue => new IssueModel
                        {
                            Number = issue["number"].ToObject<int>()
                        }).ToList();
                var expectedString = expected.ToReadableString();

                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery.ResultBuilder);
                var actualString = actual.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_Subquery_Expression_PageInfo()
            {
                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery.PageInfo);
                var actualString = actual.ToReadableString();

                Expression<Func<JObject, JToken>> expected = data => data.SelectToken("data.node.issues.pageInfo");
                var expectedString = expected.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_Subquery_Expression_ParentId()
            {
                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery.ParentIds);
                var actualString = actual.ToReadableString();

                Expression<Func<JObject, IEnumerable<JToken>>> expected = (Expression<Func<JObject, IEnumerable<JToken>>>)(data => data.SelectTokens("$.data.repository.id"));
                var expectedString = expected.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_Subquery_Expression_ParentPageInfo()
            {
                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubquery.ParentPageInfo);
                var actualString = actual.ToReadableString();

                Expression<Func<JObject, IEnumerable<JToken>>> expected = (Expression<Func<JObject, IEnumerable<JToken>>>)(data => data.SelectTokens("$.data.repository.issues.pageInfo"));
                var expectedString = expected.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public async Task Reads_All_Pages()
            {
                int page = 0;

                string Execute(string query, IDictionary<string, string> variables)
                {
                    switch (page++)
                    {
                        case 0:
                            Assert.Null(variables);
                            return @"{
  data: {
    ""repository"": {
      ""id"": ""repoid"",
      ""name"": ""foo"",
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
          ""hasNextPage"": false,
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
                        default:
                            throw new NotSupportedException("Should not get here");
                    }
                }

                var connection = new MockConnection(Execute);
                var result = await connection.Run(TestQuery);

                Assert.Equal(
                    Enumerable.Range(0, 5).ToList(),
                    result.Issues.Select(x => x.Key).ToList());
            }
        }

        public class Repository_Issues_Comments_AllPages
        {
            static Repository_Issues_Comments_AllPages()
            {
                ExpressionCompiler.IsUnitTesting = true;
            }

            private ICompiledQuery<IEnumerable<IssueModel>> TestQuery { get; } = new Query()
                .Repository("foo", "bar")
                .Issues()
                .AllPages()
                .Select(issue => new IssueModel
                {
                    Number = issue.Number,
                    Comments = issue.Comments(null, null, null, null).AllPages().Select(comment => new CommentModel
                    {
                        Id = comment.Id.ToString(),
                        Body = comment.Body,
                    }).ToList(),
                })
                .Compile();

            private SimpleQuery<IEnumerable<IssueModel>> TestMasterQuery => TestQuery.GetMasterQuery();

            private IReadOnlyList<ISubquery> TestQuerySubqueries => TestQuery.GetSubqueries();

            private PagedSubquery<IEnumerable<IssueModel>> TestQueryFirstSubquery => (PagedSubquery<IEnumerable<IssueModel>>)TestQuerySubqueries.First();

            private SimpleQuery<IEnumerable<IssueModel>> TestQueryFirstSubqueryMasterQuery => TestQueryFirstSubquery.GetMasterQuery();

            private IReadOnlyList<ISubquery> TestQueryFirstSubquerySubqueries => TestQueryFirstSubquery.GetSubqueries();

            private SimpleSubquery<IEnumerable<CommentModel>> TestQueryFirstSubqueryFirstSubquery => (SimpleSubquery<IEnumerable<CommentModel>>)TestQueryFirstSubquerySubqueries[0];

            private SimpleSubquery<IEnumerable<CommentModel>> TestQuerySecondSubquery => (SimpleSubquery<IEnumerable<CommentModel>>)TestQuerySubqueries.Skip(1).First();

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
        id
        number
        comments(first: 100) {
          pageInfo {
            hasNextPage
            endCursor
          }
          nodes {
            id
            body
          }
        }
      }
    }
  }
}";

                Assert.Equal(expected, TestMasterQuery.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_MasterQuery_Expression()
            {
                Expression<Func<JObject, IEnumerable<IssueModel>>> expected = data =>
                    (IEnumerable<IssueModel>)Rewritten.List.ToSubqueryList(
                        Rewritten.List.Select(
                            data["data"]["repository"]["issues"]["nodes"],
                            issue => new IssueModel
                            {
                                Number = issue["number"].ToObject<int>(),
                                Comments = Rewritten.List.ToSubqueryList(
                                    Rewritten.List.Select(
                                        issue["comments"]["nodes"],
                                        comment => new CommentModel
                                        {
                                            Id = comment["id"].ToString(),
                                            Body = comment["body"].ToObject<string>(),
                                        }),
                                    data.Annotation<ISubqueryRunner>(),
                                    subqueryPlaceholder)
                            }),
                        data.Annotation<ISubqueryRunner>(),
                        subqueryPlaceholder);

                ExpressionRewriterAssertions.AssertCompiledQueryExpressionEqual(expected, TestMasterQuery,
                    "SimpleSubquery<IEnumerable<PagingTests.CommentModel>>",
                    "PagedSubquery<IEnumerable<PagingTests.IssueModel>>");
            }

            [Fact]
            public void Creates_PagedSubquery_1_MasterQuery()
            {
                Assert.Equal(2, TestQuerySubqueries.Count);

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
          id
          number
          comments(first: 100) {
            pageInfo {
              hasNextPage
              endCursor
            }
            nodes {
              id
              body
            }
          }
        }
      }
    }
  }
}";

                Assert.Equal(expected, TestQueryFirstSubqueryMasterQuery.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_PagedSubquery_1_MasterQuery_Expression()
            {
                Expression<Func<JObject, IEnumerable<IssueModel>>> expected =
                    data => (IEnumerable<IssueModel>)Rewritten.List.Select(
                        Rewritten.Interface.Cast(data["data"]["node"], "Repository")["issues"]["nodes"],
                        issue => new IssueModel
                        {
                            Number = issue["number"].ToObject<int>(),
                            Comments = Rewritten.List.ToSubqueryList(
                                Rewritten.List.Select(
                                    issue["comments"]["nodes"],
                                    comment => new PagingTests.CommentModel
                                    {
                                        Id = comment["id"].ToString(),
                                        Body = comment["body"].ToObject<string>()
                                    }),
                                data.Annotation<ISubqueryRunner>(),
                                subqueryPlaceholder)
                        }).ToList();
                var expectedString = ExpressionRewriterAssertions.ReplaceSubqueryPlaceholders(expected.ToReadableString(), "SimpleSubquery<IEnumerable<PagingTests.CommentModel>>");

                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubqueryMasterQuery.ResultBuilder);
                var actualString = actual.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_PagedSubquery_1_SubQuery_Expression()
            {
                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubqueryFirstSubquery.ResultBuilder);
                var actualString = actual.ToReadableString();

                Expression<Func<JObject, IEnumerable<CommentModel>>> expected =
                    data => (IEnumerable<CommentModel>)Rewritten.List.Select(
                        Rewritten.Interface.Cast(data["data"]["node"], "Issue")["comments"]["nodes"],
                        comment => new CommentModel
                        {
                            Id = comment["id"].ToString(),
                            Body = comment["body"].ToObject<string>()
                        }).ToList();
                var expectedString = expected.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_PagedSubquery_1_SubQuery_Expression_PageInfo()
            {
                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubqueryFirstSubquery.PageInfo);
                var actualString = actual.ToReadableString();

                Expression<Func<JObject, JToken>> expected = data => data.SelectToken("data.node.comments.pageInfo");
                var expectedString = expected.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_PagedSubquery_1_SubQuery_Expression_ParentIds()
            {
                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubqueryFirstSubquery.ParentIds);
                var actualString = actual.ToReadableString();

                Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.node.issues.nodes.[*].id");
                var expectedString = expected.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_PagedSubquery_1_SubQuery_Expression_ParentPageInfo()
            {
                var actual = ExpressionCompiler.GetSourceExpression(TestQueryFirstSubqueryFirstSubquery.ParentPageInfo);
                var actualString = actual.ToReadableString();

                Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.node.issues.nodes.[*].comments.pageInfo");
                var expectedString = expected.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public void Creates_Subquery_2_MasterQuery()
            {
                var expected = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    __typename
    ... on Issue {
      comments(first: 100, after: $__after) {
        pageInfo {
          hasNextPage
          endCursor
        }
        nodes {
          id
          body
        }
      }
    }
  }
}";

                Assert.Equal(2, TestQuerySubqueries.Count);
                Assert.Equal(expected, TestQuerySecondSubquery.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_Subquery_2_MasterQuery_Expression()
            {
                Expression<Func<JObject, IEnumerable<CommentModel>>> expected =
                    data => (IEnumerable<CommentModel>)Rewritten.List.Select(
                        Rewritten.Interface.Cast(data["data"]["node"], "Issue")["comments"]["nodes"],
                        comment => new CommentModel
                        {
                            Id = comment["id"].ToString(),
                            Body = comment["body"].ToObject<string>()
                        }).ToList();
                var expectedString = expected.ToReadableString();

                var actual = ExpressionCompiler.GetSourceExpression(TestQuerySecondSubquery.ResultBuilder);
                var actualString = actual.ToReadableString();

                Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
            }

            [Fact]
            public async Task Reads_All_Pages()
            {
                int page = 0;

                string CreateComment(int index)
                {
                    return @"
                { 
                  id: ""comment" + index + @""",
                  body: ""comment " + index + @""" 
                }";
                }

                string CreateIssue(int number, int commentCount, bool hasNextPage)
                {
                    return @"
          {
            id: ""issue" + number + @""",
            number: " + number + @",
            comments: {
              ""pageInfo"": {
                ""hasNextPage"": " + hasNextPage.ToString().ToLower() + @",
                ""endCursor"": """ + "comment_end" + number + @"""
              },
              ""nodes"": [" +
                string.Join(",", Enumerable.Range(number * 10, commentCount).Select(x => CreateComment(x))) + @"
              ]
            }
          }";
                }

                string Execute(string query, IDictionary<string, string> variables)
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
          ""endCursor"": ""issue_end0""
        },
        ""nodes"": [" +
            CreateIssue(1, 1, false) + "," +
            CreateIssue(2, 3, true) + "," +
            CreateIssue(3, 0, false) + @"
        ]
      }
    }
  }
}";
                        case 1:
                            Assert.NotNull(variables);
                            Assert.Equal("issue2", variables["__id"]);
                            Assert.Equal("comment_end2", variables["__after"]);
                            return @"{
  data: {
    ""node"": {
      ""__typename"": ""Issue"",
      ""comments"": {
        ""pageInfo"": {
          ""hasNextPage"": false,
          ""endCursor"": ""comment_end""
        },
        ""nodes"": [" +
            CreateComment(23) + "," +
            CreateComment(24) + @"
        ]
      }
    }
  }
}";

                        case 2:
                            Assert.NotNull(variables);
                            Assert.Equal("repoid", variables["__id"]);
                            Assert.Equal("issue_end0", variables["__after"]);
                            return @"{
  data: {
    ""node"": {
      ""__typename"": ""Repository"",
      ""issues"": {
        ""pageInfo"": {
          ""hasNextPage"": false,
          ""endCursor"": ""issue_end1""
        },
        ""nodes"": [" +
            CreateIssue(4, 0, false) + "," +
            CreateIssue(5, 0, false) + @"
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
                var result = (await connection.Run(TestQuery)).ToList();

                Assert.Equal(Enumerable.Range(1, 5).ToList(), result.Select(x => x.Number).ToList());
                Assert.Single(result[0].Comments);
                Assert.Equal(5, result[1].Comments.Count);
                Assert.Empty(result[2].Comments);
                Assert.Empty(result[3].Comments);
                Assert.Empty(result[4].Comments);
            }

            [Fact]
            public async Task Handles_Empty_Nodes()
            {
                int page = 0;

                string Execute(string query, IDictionary<string, string> variables)
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
          ""hasNextPage"": false,
          ""endCursor"": ""issue_end0""
        },
        ""nodes"": []
      }
    }
  }
}";
                        default:
                            throw new NotSupportedException("Should not get here");
                    }
                }

                var connection = new MockConnection(Execute);
                var result = (await connection.Run(TestQuery)).ToList();

                Assert.Empty(result);
            }
        }

        public class Repository_Issues_ComplexQuery_AllPages
        {
            static Repository_Issues_ComplexQuery_AllPages()
            {
                ExpressionCompiler.IsUnitTesting = true;
            }

            [Fact]
            public void Creates_MasterQuery()
            {
                var masterQuery = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    id
    stringList1: issues(first: 100) {
      pageInfo {
        hasNextPage
        endCursor
      }
      nodes {
        number
      }
    }
    stringList2: issues(first: 100) {
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

                var subQuery = @"query($__id: ID!, $__after: String) {
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

                var compiledQuery = new Query()
                    .Repository("foo", "bar")
                    .Select(repository => new
                    {
                        StringList1 = repository.Issues(null, null, null, null, null)
                            .AllPages()
                            .Select(issue => issue.Number)
                            .ToList(),
                        StringList2 = repository.Issues(null, null, null, null, null)
                            .AllPages()
                            .Select(issue => issue.Number)
                            .ToList()
                    }).Compile();

                var master = compiledQuery.GetMasterQuery();

                Assert.Equal(masterQuery, master.ToString(), ignoreLineEndingDifferences: true);

                var subqueries = compiledQuery.GetSubqueries();

                Assert.Equal(subQuery, subqueries[0].ToString(), ignoreLineEndingDifferences: true);
                Assert.Equal(subQuery, subqueries[1].ToString(), ignoreLineEndingDifferences: true);
            }
        }

        class RepositoryModel
        {
            public string Name { get; set; }
            public IList<IssueModel> Issues { get; set; }
        }

        class RepositoryModelWithDictionary
        {
            public string Name { get; set; }
            public IDictionary<int, IssueModel> Issues { get; set; }
        }

        class IssueModel
        {
            public int Number { get; set; }
            public IList<CommentModel> Comments { get; set; }
        }

        class CommentModel
        {
            public string Id { get; set; }
            public string Body { get; set; }
        }
    }
}
