using System;
using System.Linq;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Introspection;
using LinqToGraphQL.Serializers;
using Xunit;

namespace LinqToGraphQL.UnitTests
{
    public class IntrospectionTests
    {
        [Fact]
        public void Select_Schema_QueryType_Kind()
        {
            var expected = "{__schema{queryType{kind}}}";

            var expression = new IntrospectionQuery()
                .Schema.QueryType
                .Select(x => x.Kind);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }
    }
}
