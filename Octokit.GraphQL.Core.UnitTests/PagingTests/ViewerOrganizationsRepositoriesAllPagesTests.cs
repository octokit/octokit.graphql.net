using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AgileObjects.ReadableExpressions;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests.Models;
using Octokit.GraphQL.Core.UnitTests.PagingTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class Viewer_Organizations_Repositories
    {
        static Viewer_Organizations_Repositories()
        {
            ExpressionCompiler.IsUnitTesting = true;
        }

        private static ICompiledQuery<ViewerRepositoriesModel> TestQuery()
        {
            var query = new Query()
                .Viewer
                .Select(viewer => new ViewerRepositoriesModel
                {
                    OrganizationRepositories = viewer.Organizations(null, null, null, null).AllPages()
                        .Select(org => new OrganizationRepository
                        {
                            Name = org.Name,
                            Repositories = org.Repositories(null, null, null, null, null, null, null, null, null, null)
                                .AllPages()
                                .Select(repo => new RepositoryListItemModel
                                {
                                    Name = repo.Name,
                                })
                                .ToList()
                        }).ToList()
                }).Compile();
            return query;
        }

        private SimpleQuery<ViewerRepositoriesModel> TestQuery_MasterQuery() => TestQuery().GetMasterQuery();

        private IReadOnlyList<ISubquery> TestQuery_Subqueries() => TestQuery().GetSubqueries();

        private PagedSubquery<IEnumerable<OrganizationRepository>> TestQuery_PagedSubquery_1() => (PagedSubquery<IEnumerable<OrganizationRepository>>)TestQuery_Subqueries()[0];

        private SimpleQuery<IEnumerable<OrganizationRepository>> TestQuery_PagedSubquery_1_MasterQuery() => TestQuery_PagedSubquery_1().GetMasterQuery();

        private IReadOnlyList<ISubquery> TestQuery_PagedSubquery_1_Subqueries() => TestQuery_PagedSubquery_1().GetSubqueries();

        private SimpleSubquery<IEnumerable<RepositoryListItemModel>> TestQuery_PagedSubquery_1_Subquery_1() => (SimpleSubquery<IEnumerable<RepositoryListItemModel>>)TestQuery_PagedSubquery_1_Subqueries()[0];

        private SimpleSubquery<IEnumerable<RepositoryListItemModel>> TestQuery_SimpleSubquery_2() => (SimpleSubquery<IEnumerable<RepositoryListItemModel>>)TestQuery_Subqueries()[1];

        [Fact]
        public void Creates_MasterQuery()
        {
            var expected = @"query {
  viewer {
    id
    organizationRepositories: organizations(first: 100) {
      pageInfo {
        hasNextPage
        endCursor
      }
      nodes {
        id
        name
        repositories(first: 100) {
          pageInfo {
            hasNextPage
            endCursor
          }
          nodes {
            name
          }
        }
      }
    }
  }
}";

            var masterQuery = TestQuery_MasterQuery();

            Assert.Equal(expected, masterQuery.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Creates_MasterQuery_Expression()
        {
            Expression<Func<JObject, object>> expected = data => Rewritten.Value.Select(
                data["data"]["viewer"],
                viewer => new ViewerRepositoriesModel
                {
                    OrganizationRepositories = Rewritten.List.ToSubqueryList(
                        Rewritten.List.Select(
                            viewer["organizationRepositories"]["nodes"],
                            org => new OrganizationRepository
                            {
                                Name = org["name"].ToObject<string>(),
                                Repositories = Rewritten.List.ToSubqueryList(
                                    Rewritten.List.Select(
                                        org["repositories"]["nodes"],
                                        repo => new RepositoryListItemModel
                                        {
                                            Name = repo["name"].ToObject<string>()
                                        }),
                                    data.Annotation<ISubqueryRunner>(),
                                    SubqueryPlaceholder.placeholder)
                            }),
                        data.Annotation<ISubqueryRunner>(),
                        SubqueryPlaceholder.placeholder)
                });

            var masterQuery = TestQuery_MasterQuery();

            ExpressionRewriterAssertions.AssertCompiledQueryExpressionEqual(expected, masterQuery,
                "SimpleSubquery<IEnumerable<RepositoryListItemModel>>",
                "PagedSubquery<IEnumerable<OrganizationRepository>>");
        }

        [Fact]
        public void Creates_PagedSubquery_1_MasterQuery()
        {
            var expected = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    __typename
    ... on User {
      organizationRepositories: organizations(first: 100, after: $__after) {
        pageInfo {
          hasNextPage
          endCursor
        }
        nodes {
          id
          name
          repositories(first: 100) {
            pageInfo {
              hasNextPage
              endCursor
            }
            nodes {
              name
            }
          }
        }
      }
    }
  }
}";

            Assert.Equal(expected, TestQuery_PagedSubquery_1_MasterQuery().ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Creates_PagedSubquery_1_SubQuery_1_Query()
        {
            var expected = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    __typename
    ... on Organization {
      repositories(first: 100, after: $__after) {
        pageInfo {
          hasNextPage
          endCursor
        }
        nodes {
          name
        }
      }
    }
  }
}";

            var actual = TestQuery_PagedSubquery_1_Subquery_1().ToString();
            Assert.Equal(expected, actual, ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Creates_PagedSubquery_1_SubQuery_1_Expression()
        {
            var actual = ExpressionCompiler.GetSourceExpression(TestQuery_PagedSubquery_1_Subquery_1().ResultBuilder);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, IEnumerable<RepositoryListItemModel>>> expected =
                data => (IEnumerable<RepositoryListItemModel>)Rewritten.List.Select(
                    Rewritten.Interface.Cast(data["data"]["node"], "Organization")["repositories"]["nodes"],
                    repo => new RepositoryListItemModel
                    {
                        Name = repo["name"].ToObject<string>()
                    }).ToList();
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_PagedSubquery_1_SubQuery_1_Expression_PageInfo()
        {
            var actual = ExpressionCompiler.GetSourceExpression(TestQuery_PagedSubquery_1_Subquery_1().PageInfo);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, JToken>> expected = data => data.SelectToken("data.node.repositories.pageInfo");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_PagedSubquery_1_SubQuery_1_Expression_ParentIds()
        {
            var actual = ExpressionCompiler.GetSourceExpression(TestQuery_PagedSubquery_1_Subquery_1().ParentIds);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.node.organizations.nodes.[*].id");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_PagedSubquery_1_SubQuery_1_Expression_ParentPageInfo()
        {
            var actual = ExpressionCompiler.GetSourceExpression(TestQuery_PagedSubquery_1_Subquery_1().ParentPageInfo);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.node.organizations.nodes.[*].repositories.pageInfo");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_SimpleSubquery_2_Query()
        {
            var expected = @"query($__id: ID!, $__after: String) {
  node(id: $__id) {
    __typename
    ... on Organization {
      repositories(first: 100, after: $__after) {
        pageInfo {
          hasNextPage
          endCursor
        }
        nodes {
          name
        }
      }
    }
  }
}";

            Assert.Equal(expected, TestQuery_SimpleSubquery_2().ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Creates_SimpleSubquery_2_Expression()
        {
            var actual = ExpressionCompiler.GetSourceExpression(TestQuery_SimpleSubquery_2().ResultBuilder);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, IEnumerable<RepositoryListItemModel>>> expected =
                data => (IEnumerable<RepositoryListItemModel>)Rewritten.List.Select(
                    Rewritten.Interface.Cast(data["data"]["node"], "Organization")["repositories"]["nodes"],
                    repo => new RepositoryListItemModel
                    {
                        Name = repo["name"].ToObject<string>()
                    }).ToList();
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_SimpleSubquery_2_Expression_PageInfo()
        {
            var actual = ExpressionCompiler.GetSourceExpression(TestQuery_SimpleSubquery_2().PageInfo);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, JToken>> expected = data => data.SelectToken("data.node.repositories.pageInfo");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_SimpleSubquery_2_Expression_ParentIds()
        {
            var actual = ExpressionCompiler.GetSourceExpression(TestQuery_SimpleSubquery_2().ParentIds);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.viewer.organizations.nodes.[*].id");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public void Creates_SimpleSubquery_2_Expression_ParentPageInfo()
        {
            var actual = ExpressionCompiler.GetSourceExpression(TestQuery_SimpleSubquery_2().ParentPageInfo);
            var actualString = actual.ToReadableString();

            Expression<Func<JObject, IEnumerable<JToken>>> expected = data => data.SelectTokens("$.data.viewer.organizationRepositories.nodes.[*].repositories.pageInfo");
            var expectedString = expected.ToReadableString();

            Assert.Equal(ExpressionRewriterAssertions.StripWhitespace(expectedString), ExpressionRewriterAssertions.StripWhitespace(actualString));
        }

        [Fact]
        public async Task Reads_All_Pages_Of_Organizations()
        {
            int page = 0;

            string Execute(string query, IDictionary<string, string> variables)
            {
                switch (page++)
                {
                    case 0:
                        Assert.Null(variables);
                        return
@"{
    ""data"":{
        ""viewer"":{
            ""id"":""viewerid"",
            ""organizationRepositories"":{
                ""pageInfo"":{
                    ""hasNextPage"":true,
                    ""endCursor"":""organizationRepositories_end_1""
                },
                ""nodes"":[" +
                    CreateOrganization(1, 2, false) + "," +
                    CreateOrganization(2, 2, false) +
                @"]
            }
        }
    }
}";
                    case 1:
                        Assert.NotNull(variables);
                        Assert.Equal("viewerid", variables["__id"]);
                        Assert.Equal("organizationRepositories_end_1", variables["__after"]);
                        return @"{
  data: {
    ""node"": {
      ""__typename"": ""User"",
      ""organizations"": {
        ""pageInfo"": {
          ""hasNextPage"": false,
          ""endCursor"": ""organizationRepositories_end_2""
        },
        ""nodes"": [" +
           CreateOrganization(3, 2, false) + "," +
           CreateOrganization(4, 2, false) +
        @"]
      }
    }
  }
}";

                    default:
                        throw new NotSupportedException("Should not get here");
                }
            }

            var connection = new MockConnection(Execute);
            var result = await connection.Run(TestQuery());
        }

        private static string CreateOrganization(int id, int repositoryCount, bool hasNextPage)
        {
            return @"{
    ""id"":""MDEyOk9yZ2FuaXphdGlvbjEwMDY4NDE2"",
    ""name"":""organization" + id + @""",
    ""repositories"":{
        ""pageInfo"":{
            ""hasNextPage"":" + hasNextPage.ToString().ToLower() + @",
            ""endCursor"":""repository_end_" + id + @"""
        },
        ""nodes"":[" +
            string.Join(",", Enumerable.Range(id * 10, repositoryCount).Select(CreateRepository))
        + @"]
    }
}";
        }

        private static string CreateRepository(int id)
        {
            return $@"{{""name"":""repository{id}""}}";
        }
    }
}