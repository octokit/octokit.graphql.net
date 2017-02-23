using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL.Builders;
using LinqToGraphQL.UnitTests.Models;
using Newtonsoft.Json.Linq;
using Xunit;

namespace LinqToGraphQL.UnitTests
{
    public class ExpressionRewiterTests
    {
        [Fact]
        public void SimpleQuery_Select_Single_Member()
        {
            var query = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name);

            Expression<Func<JObject, string>> expected = x =>
                x["data"]["simple"]["name"].ToObject<string>();

            var operation = new QueryBuilder().Build(query);
            Assert.Equal(expected.ToString(), operation.Expression.ToString());
        }
    }
}
