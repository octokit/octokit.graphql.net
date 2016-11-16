using System;
using System.Linq;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Deserializers;
using LinqToGraphQL.UnitTests.Models;
using Xunit;

namespace LinqToGraphQL.UnitTests
{
    public class ResponseDeserializerTests
    {
        [Fact]
        public void SimpleQuery_Select_Single_Member()
        {
            var query = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name);
            var data = @"{
  ""data"":{
    ""simple"":{
      ""name"": ""Hello World!""
    }
  }
}";

            var operation = new QueryBuilder().Build(query.Expression);
            var result = new ResponseDeserializer().Deserialize(operation, data);

            Assert.IsType<string>(result);
            Assert.Equal("Hello World!", result);
        }

        [Fact]
        public void SimpleQuery_Select_Multiple_Members()
        {
            var query = new RootQuery()
                .Simple("foo", 2)
                .Select(x => new { x.Name, x.Description });
            var data = @"{
  ""data"":{
    ""simple"":{
      ""name"": ""Hello World!"",
      ""description"": ""Goodbye cruel world""
    }
  }
}";

            var operation = new QueryBuilder().Build(query.Expression);
            var result = new ResponseDeserializer().Deserialize(operation, data);

            var expectedType = query.GetType().GetGenericArguments()[0];
            Assert.IsType(expectedType, result);
            //Assert.Equal("Hello World!", result);
        }
    }
}
