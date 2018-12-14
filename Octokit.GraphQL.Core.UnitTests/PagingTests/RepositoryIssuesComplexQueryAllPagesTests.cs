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
    public class Repository_Issues_ComplexQuery_AllPages
    {
        static Repository_Issues_ComplexQuery_AllPages()
        {
            ExpressionCompiler.IsUnitTesting = true;
        }

        [Fact]
        public void Creates_MasterQuery()
        {
            var expected = @"query {
  repository(owner: ""foo"", name: ""bar"") {
    id
    intList1: issues(first: 100) {
      pageInfo {
        hasNextPage
        endCursor
      }
      nodes {
        number
      }
    }
    intList2: issues(first: 100) {
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

            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();

            var masterQuery = query.GetMasterQuery();

            Assert.Equal(expected, masterQuery.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Creates_MasterQuery_Expression()
        {
            Expression<Func<JObject, object>> expected = data => 
                Rewritten.Value.Select(
                    data["data"]["repository"],
                    repository => new {
                        IntList1 = Rewritten.List.ToSubqueryList(
                            Rewritten.List.ToList<int>(Rewritten.List.Select(repository["intList1"]["nodes"], issue => issue["number"])),
                            data.Annotation<ISubqueryRunner>(),
                            SubqueryPlaceholder.placeholder),
                        IntList2 = Rewritten.List.ToSubqueryList(
                            Rewritten.List.ToList<int>(Rewritten.List.Select(repository["intList2"]["nodes"], issue => issue["number"])),
                            data.Annotation<ISubqueryRunner>(),
                            SubqueryPlaceholder.placeholder)
                    });

            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();

            var masterQuery = query.GetMasterQuery();

            ExpressionRewriterAssertions.AssertCompiledQueryExpressionEqual(expected, masterQuery,
                "SimpleSubquery<IEnumerable<int>>",
                "SimpleSubquery<IEnumerable<int>>");
        }

        [Fact]
        public void Creates_Subquery_1_Expression()
        {
            Expression<Func<JObject, IEnumerable<int>>> expected =
                data => (IEnumerable<int>)Rewritten.List.Select(
                    Rewritten.Interface.Cast(data["data"]["node"], "Repository")["issues"]["nodes"],
                    issue =>  issue["number"].ToObject<int>()).ToList();
            var expectedString = expected.ToReadableString();

            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();

            var subqueries = query.GetSubqueries();
            var queryFirstSubquery = (SimpleSubquery<IEnumerable<int>>) subqueries[0];

            var actual = ExpressionCompiler.GetSourceExpression(queryFirstSubquery.ResultBuilder);
            var actualString = actual.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_Subquery_1_Expression_PageInfo()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();

            var subqueries = query.GetSubqueries();
            var queryFirstSubquery = (SimpleSubquery<IEnumerable<int>>)subqueries[0];

            var actual = ExpressionCompiler.GetSourceExpression(queryFirstSubquery.PageInfo);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, JToken>> expected = data => data.SelectToken("data.node.issues.pageInfo");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_Subquery_1_Expression_ParentId()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();

            var subqueries = query.GetSubqueries();
            var queryFirstSubquery = (SimpleSubquery<IEnumerable<int>>)subqueries[0];

            var actual = ExpressionCompiler.GetSourceExpression(queryFirstSubquery.ParentIds);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.repository.id");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_Subquery_1_Expression_ParentPageInfo()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();

            var subqueries = query.GetSubqueries();
            var queryFirstSubquery = (SimpleSubquery<IEnumerable<int>>)subqueries[0];

            var actual = ExpressionCompiler.GetSourceExpression(queryFirstSubquery.ParentPageInfo);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.repository.intList1.pageInfo");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_Subquery_2_Expression()
        {
            Expression<Func<JObject, IEnumerable<int>>> expected =
                data => (IEnumerable<int>)Rewritten.List.Select(
                    Rewritten.Interface.Cast(data["data"]["node"], "Repository")["issues"]["nodes"],
                    issue =>  issue["number"].ToObject<int>()).ToList();
            var expectedString = expected.ToReadableString();

            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();

            var subqueries = query.GetSubqueries();
            var queryFirstSubquery = (SimpleSubquery<IEnumerable<int>>) subqueries[1];

            var actual = ExpressionCompiler.GetSourceExpression(queryFirstSubquery.ResultBuilder);
            var actualString = actual.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_Subquery_2_Expression_PageInfo()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();

            var subqueries = query.GetSubqueries();
            var queryFirstSubquery = (SimpleSubquery<IEnumerable<int>>)subqueries[1];

            var actual = ExpressionCompiler.GetSourceExpression(queryFirstSubquery.PageInfo);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, JToken>> expected = data => data.SelectToken("data.node.issues.pageInfo");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_Subquery_2_Expression_ParentId()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();

            var subqueries = query.GetSubqueries();
            var queryFirstSubquery = (SimpleSubquery<IEnumerable<int>>)subqueries[1];

            var actual = ExpressionCompiler.GetSourceExpression(queryFirstSubquery.ParentIds);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.repository.id");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_Subquery_2_Expression_ParentPageInfo()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();

            var subqueries = query.GetSubqueries();
            var queryFirstSubquery = (SimpleSubquery<IEnumerable<int>>)subqueries[1];

            var actual = ExpressionCompiler.GetSourceExpression(queryFirstSubquery.ParentPageInfo);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.repository.intList2.pageInfo");
            var expectedString = expected.ToReadableString();

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
	""data"": {
		""repository"": {
			""id"": ""repoId"",
			""intList1"": {
				""pageInfo"": {
					""hasNextPage"": true,
					""endCursor"": ""endCursor1""
				},
				""nodes"": [{
						""number"": 1
					}, {
						""number"": 2
					}, {
						""number"": 3
					}
				]
			},
			""intList2"": {
				""pageInfo"": {
					""hasNextPage"": true,
					""endCursor"": ""endCursor2""
				},
				""nodes"": [{
						""number"": 4
					}, {
						""number"": 5
					}, {
						""number"": 6
					}
				]
			}
		}
	}
}";
                    case 1:
                        Assert.NotNull(variables);
                        Assert.Equal("repoId", variables["__id"]);
                        Assert.Equal("endCursor2", variables["__after"]);

                        return @"{
	""data"": {
		""node"": {
			""__typename"": ""Repository"",
			""issues"": {
				""pageInfo"": {
					""hasNextPage"": false,
					""endCursor"": ""endCursor2.1""
				},
				""nodes"": [{
						""number"": 4
					}, {
						""number"": 5
					}
				]
			}
		}
	}
}";
                    case 2:
                        Assert.NotNull(variables);
                        Assert.Equal("repoId", variables["__id"]);
                        Assert.Equal("endCursor1", variables["__after"]);

                        return @"{
	""data"": {
		""node"": {
			""__typename"": ""Repository"",
			""issues"": {
				""pageInfo"": {
					""hasNextPage"": false,
					""endCursor"": ""endCursor1.1""
				},
				""nodes"": [{
						""number"": 4
					}, {
						""number"": 5
					}, {
						""number"": 6
					}
				]
			}
		}
	}
}";

                    default:
                        throw new NotSupportedException("Should not get here");
                }
            }

            var query = new Query()
                .Repository("foo", "bar")
                .Select(repository => new
                {
                    IntList1 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList(),
                    IntList2 = repository.Issues(null, null, null, null, null)
                        .AllPages()
                        .Select(issue => issue.Number)
                        .ToList()
                }).Compile();


            var connection = new MockConnection(Execute);
            var result = (await connection.Run(query));

            Assert.Equal(6, result.IntList1.Count);
            Assert.Equal(5, result.IntList2.Count);
        }
    }
}