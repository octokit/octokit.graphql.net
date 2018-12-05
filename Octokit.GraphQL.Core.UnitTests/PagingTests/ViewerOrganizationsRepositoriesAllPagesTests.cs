using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        private SimpleQuery<ViewerRepositoriesModel> TestMasterQuery() => TestQuery().GetMasterQuery();

        private IReadOnlyList<ISubquery> TestQuerySubqueries() => TestQuery().GetSubqueries();

        private PagedSubquery<IEnumerable<OrganizationRepository>> TestQueryFirstSubquery() => (PagedSubquery<IEnumerable<OrganizationRepository>>)TestQuerySubqueries().First();

        private SimpleQuery<IEnumerable<OrganizationRepository>> TestQueryFirstSubqueryMasterQuery() => TestQueryFirstSubquery().GetMasterQuery();

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

            var masterQuery = TestMasterQuery();

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

            var masterQuery = TestMasterQuery();

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
      organizations(first: 100, after: $__after) {
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

            Assert.Equal(expected, TestQueryFirstSubqueryMasterQuery().ToString(), ignoreLineEndingDifferences: true);
        }
    }
}