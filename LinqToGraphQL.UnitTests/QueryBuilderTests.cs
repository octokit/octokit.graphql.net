using System;
using System.Linq;
using LinqToGraphQL.Builders;
using LinqToGraphQL.UnitTests.Models;
using Xunit;

namespace LinqToGraphQL.UnitTests
{
    public class QueryBuilderTests
    {
        [Fact]
        public void SimpleQuery_Select_Single_Member()
        {
            var expected = "query RootQuery{simple(arg1:\"foo\"){name}}";
            var query = new RootQuery().Simple("foo").Select(x => x.Name);
            var result = new QuerySerializer().Serialize(new QueryBuilder().Build(query.Expression));

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Select_Multiple_Members()
        {
            var expected = "query RootQuery{simple(arg1:\"foo\",arg2:2){name description}}";
            var query = new RootQuery().Simple("foo", 2).Select(x => new { x.Name, x.Description });
            var result = new QuerySerializer().Serialize(new QueryBuilder().Build(query.Expression));

            Assert.Equal(expected, result);
        }
    }
}
