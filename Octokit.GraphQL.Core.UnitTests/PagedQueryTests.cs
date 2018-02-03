using System;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.Deserializers;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class PagedQueryTests
    {
        [Fact]
        public void Builds_Page_Expression_For_PagesOfNested()
        {
            var query = new TestQuery()
                .PagesOfNested(PageOption.Second)
                .Select(x => x.Name);

            var expected = ExpectedExpression.Normalize(() =>
                new TestQuery()
                    .PagesOfNestedInternal(
                        new Arg<int>("first", 0),
                        new Arg<string>("after", null),
                        new Arg<int>("last", 0),
                        new Arg<string>("before", null),
                        new Arg<PageOption>(null, PageOption.Second))
                    .Select(connection => new Page<string>
                    {
                        HasNextPage = connection.PageInfo.HasNextPage,
                        EndCursor = connection.PageInfo.EndCursor,
                        Items = connection.Nodes.Select(x => x.Name).ToList(),
                    }));

            var result = new PagedQueryBuilder().RewriteExpression(query.Expression);
            var normalized = ExpectedExpression.Normalize(result);

            Assert.Equal(expected.ToString(), normalized.ToString());
        }

        [Fact]
        public void Builds_Master_Query_For_Paged_Inner_Query()
        {
            var expected = @"query($first: Int, $after: String, $last: Int, $before: String) {
  simple(arg1: ""foo"") {
    name
    pagesOfNested(first: $first, after: $after, last: $last, before: $before, option: FIRST) {
      pageInfo {
        hasNextPage
        endCursor
      }
      nodes {
        name
      }
    }
  }
}";

            var expression = new TestQuery()
                .Simple("foo")
                .Select(x => new
                {
                    x.Name,
                    Nested = x.PagesOfNested(PageOption.First).Select(y => new
                    {
                        y.Name
                    }).ToList(),
                });

            var query = new QueryBuilder().Build(expression);

            Assert.Equal(expected, query.ToString());
        }

        [Fact]
        public void Deserializes_Master_Query_Data_For_Paged_Inner_Query()
        {
            var expression = new TestQuery()
                .Simple("foo")
                .Select(x => new
                {
                    x.Name,
                    Nested = x.PagesOfNested(PageOption.First).Select(y => y.Name).ToList(),
                });

            var data = @"{
  ""data"":{
    ""simple"":{
      ""name"": ""Hello World!"",
      ""pagesOfNested"": {
        ""pageInfo"": {
          ""hasNextPage"": ""true"",
          ""endCursor"": ""123abc"",
        },
        ""nodes"": [
          { ""name"": ""foo"" }, 
          { ""name"": ""bar"" }
        ]
      }
    }
  }
}";

            var query = new QueryBuilder().Build(expression);

            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal("Hello World!", result.Name);
            Assert.Equal(2, result.Nested.Count);
            Assert.Equal("foo", result.Nested[0]);
            Assert.Equal("bar", result.Nested[1]);

            var page = (Page<string>)result.Nested;
            Assert.Equal(true, page.HasNextPage);
            Assert.Equal("123abc", page.EndCursor);
        }
    }
}
