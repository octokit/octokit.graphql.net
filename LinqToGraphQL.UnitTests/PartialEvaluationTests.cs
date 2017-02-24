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
            var query = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name + " World!");

            var data = @"{
  ""data"":{
    ""simple"":{
      ""name"": ""Hello""
    }
  }
}";

            var operation = new QueryBuilder().Build(query);
            var expectedType = query.GetType().GetGenericArguments()[0];
            var result = new ResponseDeserializer().Deserialize(operation, data).Single();

            Assert.IsType(expectedType, result);
            Assert.Equal("Hello World!", result);
        }
    }
}
