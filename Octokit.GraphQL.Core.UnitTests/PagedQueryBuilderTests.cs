using System;
using System.Threading.Tasks;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class PagedQueryBuilderTests
    {
        [Fact]
        public void Builds_Page_Expression_For_PagesOfSimple()
        {
            var expression = new TestQuery()
                .PagesOfSimple(true)
                .Select(x => x.Name);

            var expected = new TestQuery()
                .PagesOfSimple(new Variable("first"), new Variable("after"), null)
                .Select(x => x.Name);

            var result = new PagedQueryBuilder().BuildPageExpression(expression);

            Assert.Equal(expected.Expression.ToString(), result.ToString());
        }
    }
}
