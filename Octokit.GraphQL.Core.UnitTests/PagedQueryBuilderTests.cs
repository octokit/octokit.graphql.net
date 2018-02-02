using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class PagedQueryBuilderTests
    {
        [Fact]
        public void Builds_Page_Expression_For_PagesOfNested()
        {
            var expression = new TestQuery()
                .PagesOfNested(PageOption.Second)
                .Select(x => x.Name);

            var expected = ExpectedExpression.Normalize(() =>
                new TestQuery()
                    .PagesOfNested(
                        new Arg<int>("first", 0),
                        new Arg<string>("after", null),
                        new Arg<PageOption>(null, PageOption.Second))
                    .Select(connection => new Page<string>
                    {
                        HasNextPage = connection.PageInfo.HasNextPage,
                        EndCursor = connection.PageInfo.EndCursor,
                        Items = connection.Nodes.Select(x => x.Name).ToList(),
                    }));

            var result = new PagedQueryBuilder().RewriteExpression(expression);
            var normalized = ExpectedExpression.Normalize(result);

            Assert.Equal(expected.ToString(), normalized.ToString());
        }
    }
}
