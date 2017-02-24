using System.Linq;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Deserializers;
using LinqToGraphQL.UnitTests.Models;
using Xunit;

namespace LinqToGraphQL.UnitTests
{
    public class PartialEvaluationTests
    {
        [Fact]
        public void SimpleQuery_Select_Single_Member_Append_String()
        {
            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name + " World!");

            var data = @"{
  ""data"":{
    ""simple"":{
      ""name"": ""Hello""
    }
  }
}";

            var query = new QueryBuilder().Build(expression);
            var expectedType = expression.GetType().GetGenericArguments()[0];
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.IsType(expectedType, result);
            Assert.Equal("Hello World!", result);
        }

        [Fact]
        public void SimpleQuery_Select_Append_Two_Members()
        {
            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name + x.Description);

            var data = @"{
  ""data"":{
    ""simple"":{
      ""name"": ""Hello"",
      ""description"": "" World!""
    }
  }
}";

            var query = new QueryBuilder().Build(expression);
            var expectedType = expression.GetType().GetGenericArguments()[0];
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.IsType(expectedType, result);
            Assert.Equal("Hello World!", result);
        }
    }
}
