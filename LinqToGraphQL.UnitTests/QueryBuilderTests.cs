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
            var expected = "query RootQuery{simple(arg:\"foo\"){name}}";
            var query = new RootQuery().Simple("foo").Select(x => x.Name);
            var result = new QuerySerializer().Serialize(new QueryBuilder().Build(query));

            Assert.Equal(expected, result);
        }
    }
}
