using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;
using static Octokit.GraphQL.Variable;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class PagingTests
    {
        static readonly ISubquery subqueryPlaceholder;

        public class Repository_Issues_AllPages
        {
            ICompiledQuery<IEnumerable<int>> TestQuery { get; } = new Query()
                .Repository("foo", "bar")
                .Issues()
                .AllPages()
                .Select(issue => issue.Number)
                .Compile();

            static Repository_Issues_AllPages()
            {
                ExpressionCompiler.IsUnitTesting = true;
            }

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

                var master = TestQuery.GetMasterQuery();

                Assert.Equal(expected, master.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact(Skip = "Need a better way to compare expressions")]
            public void Creates_MasterQuery_Expression()
            {
                var expected = Expected(data =>
                    (IEnumerable<int>)Rewritten.List.ToSubqueryList(
                        Rewritten.List.Select(
                            data["data"]["repository"]["issues"]["nodes"],
                            issue => issue["number"].ToObject<int>()),
                        data.Annotation<ISubqueryRunner>(),
                        subqueryPlaceholder));

                var master = TestQuery.GetMasterQuery();

                Assert.Equal(expected, master.GetResultBuilderExpression().ToString());
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

                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);
                Assert.Equal(expected, subqueries[0].ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_Subquery_ParentIds_Selector()
            {
                var expected = Expected(data => data.SelectTokens("$.data.repository.id"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[0].ParentIds);
                Assert.Equal(expected, actual.ToString());
            }

            [Fact]
            public void Creates_Subquery_PageInfo_Selector()
            {
                var expected = Expected(data => data.SelectToken("data.node.issues.pageInfo"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[0].PageInfo);
                Assert.Equal(expected, actual.ToString());
            }

            [Fact]
            public void Creates_Subquery_ParentPageInfo_Selector()
            {
                var expected = Expected(data => data.SelectTokens("$.data.repository.issues.pageInfo"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[0].ParentPageInfo);
                Assert.Equal(expected, actual.ToString());
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
                var result = (await connection.Run(TestQuery)).ToList();

                Assert.Equal(Enumerable.Range(0, 6).ToList(), result);
            }
        }

        public class Repository_Name_Issues_AllPages
        {
            ICompiledQuery<RepositoryModel> TestQuery { get; } = new Query()
                .Repository("foo", "bar")
                .Select(repository => new RepositoryModel
                {
                    Name = repository.Name,
                    Issues = repository.Issues(null, null, null, null, new[] { "bug" }).AllPages().Select(issue => new IssueModel
                    {
                        Number = issue.Number,
                    }).ToList()
                }).Compile();

            static Repository_Name_Issues_AllPages()
            {
                ExpressionCompiler.IsUnitTesting = true;
            }

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

                var master = TestQuery.GetMasterQuery();

                Assert.Equal(expected, master.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact(Skip = "Need a better way to compare expressions")]
            public void Creates_MasterQuery_Expression()
            {
                var expected = Expected(data =>
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
                        }));

                var master = TestQuery.GetMasterQuery();

                Assert.Equal(expected, master.GetResultBuilderExpression().ToString());
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

                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);
                Assert.Equal(expected, subqueries[0].ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_Subquery_PageInfo_Selector()
            {
                var expected = Expected(data => data.SelectToken("data.node.issues.pageInfo"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[0].PageInfo);
                Assert.Equal(expected, actual.ToString());
            }

            [Fact]
            public void Creates_Subquery_ParentPageInfo_Selector()
            {
                var expected = Expected(data => data.SelectTokens("$.data.repository.issues.pageInfo"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[0].ParentPageInfo);
                Assert.Equal(expected, actual.ToString());
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

        public class Repository_Issues_Comments_AllPages
        {
            ICompiledQuery<IEnumerable<IssueModel>> TestQuery { get; } = new Query()
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

            static Repository_Issues_Comments_AllPages()
            {
                ExpressionCompiler.IsUnitTesting = true;
            }

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

                var master = TestQuery.GetMasterQuery();

                Assert.Equal(expected, master.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact(Skip = "Need a better way to compare expressions")]
            public void Creates_MasterQuery_Expression()
            {
                var expected = Expected(data =>
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
                                            Body = comment["body"].ToObject<string>(),
                                        }),
                                    data.Annotation<ISubqueryRunner>(),
                                    subqueryPlaceholder)
                            }),
                        data.Annotation<ISubqueryRunner>(),
                        subqueryPlaceholder));

                var master = TestQuery.GetMasterQuery();

                Assert.Equal(expected, master.GetResultBuilderExpression().ToString());
            }

            [Fact]
            public void Creates_Subquery_1()
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

                var subqueries = TestQuery.GetSubqueries();

                Assert.Equal(2, subqueries.Count);
                Assert.IsType<PagedSubquery<IEnumerable<IssueModel>>>(subqueries[0]);

                var query = (PagedSubquery<IEnumerable<IssueModel>>)subqueries[0];
                Assert.Equal(expected, query.GetMasterQuery().ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_Subquery_1_PageInfo_Selector()
            {
                var expected = Expected(data => data.SelectToken("data.node.issues.pageInfo")); ;
                var subqueries = TestQuery.GetSubqueries();

                Assert.Equal(2, subqueries.Count);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[0].PageInfo);
                Assert.Equal(expected, actual.ToString());
            }

            [Fact]
            public void Creates_Subquery_1_ParentPageInfo_Selector()
            {
                var expected = Expected(data => data.SelectTokens("$.data.repository.issues.pageInfo"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Equal(2, subqueries.Count);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[0].ParentPageInfo);
                Assert.Equal(expected, actual.ToString());
            }

            [Fact]
            public void Creates_Subquery_2()
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

                var subqueries = TestQuery.GetSubqueries();

                Assert.Equal(2, subqueries.Count);
                Assert.Equal(expected, subqueries[1].ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_Subquery_2_PageInfo_Selector()
            {
                var expected = Expected(data => data.SelectToken("data.node.comments.pageInfo"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Equal(2, subqueries.Count);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[1].PageInfo);
                Assert.Equal(expected, actual.ToString());
            }

            [Fact]
            public void Creates_Subquery_2_ParentPageInfo_Selector()
            {
                var expected = Expected(data => data.SelectTokens("$.data.repository.issues.nodes.[*].comments.pageInfo"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Equal(2, subqueries.Count);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[1].ParentPageInfo);
                Assert.Equal(expected, actual.ToString());
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
            CreateIssue(4, 0, false) + ","  +
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

        public class AddComment_Reactions
        {
            ICompiledQuery<IEnumerable<ReactionModel>> TestQuery { get; } = new Mutation()
                .AddComment(Var("input"))
                .Select(x => x.CommentEdge.Node.Reactions(null, null, null, null).AllPages().Select(reaction => new ReactionModel
                {
                    Id = reaction.Id.Value,
                })).Compile();

            static AddComment_Reactions()
            {
                ExpressionCompiler.IsUnitTesting = true;
            }

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

                var master = TestQuery.GetMasterQuery();

                Assert.Equal(expected, master.ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact(Skip = "Need a better way to compare expressions")]
            public void Creates_MasterQuery_Expression()
            {
                var expected = Expected(data =>
                    (IEnumerable<int>)Rewritten.List.ToSubqueryList(
                        Rewritten.List.Select(
                            data["data"]["repository"]["issues"]["nodes"],
                            issue => issue["number"].ToObject<int>()),
                        data.Annotation<ISubqueryRunner>(),
                        subqueryPlaceholder));

                var master = TestQuery.GetMasterQuery();

                Assert.Equal(expected, master.GetResultBuilderExpression().ToString());
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

                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);
                Assert.Equal(expected, subqueries[0].ToString(), ignoreLineEndingDifferences: true);
            }

            [Fact]
            public void Creates_Subquery_ParentIds_Selector()
            {
                var expected = Expected(data => data.SelectTokens("$.data.repository.id"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[0].ParentIds);
                Assert.Equal(expected, actual.ToString());
            }

            [Fact]
            public void Creates_Subquery_PageInfo_Selector()
            {
                var expected = Expected(data => data.SelectToken("data.node.issues.pageInfo"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[0].PageInfo);
                Assert.Equal(expected, actual.ToString());
            }

            [Fact]
            public void Creates_Subquery_ParentPageInfo_Selector()
            {
                var expected = Expected(data => data.SelectTokens("$.data.repository.issues.pageInfo"));
                var subqueries = TestQuery.GetSubqueries();

                Assert.Single(subqueries);

                var actual = ExpressionCompiler.GetSourceExpression(subqueries[0].ParentPageInfo);
                Assert.Equal(expected, actual.ToString());
            }
        }

        private static string Expected<T>(Expression<Func<JObject, T>> expression)
        {
            var str = expression.ToString();

            // There's no way to get "value(Octokit.GraphQL.Core.Subquery)" string in an expression
            // string from a lambda, so put in `subqueryPlaceholder` and replace it.
            str = str.Replace("PagingTests.subqueryPlaceholder", "value(Octokit.GraphQL.Core.Subquery)");

            return str;
        }

        class RepositoryModel
        {
            public string Name { get; set; }
            public IList<IssueModel> Issues { get; set; }
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

        class ReactionModel
        {
            public string Id { get; set; }
        }
    }
}
