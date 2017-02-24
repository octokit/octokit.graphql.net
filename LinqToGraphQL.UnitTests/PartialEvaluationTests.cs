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
        public void Append_String()
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
        public void Append_Number()
        {
            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name + x.Number);

            var data = @"{
  ""data"":{
    ""simple"":{
      ""name"": ""Hello"",
      ""number"": 5
    }
  }
}";

            var query = new QueryBuilder().Build(expression);
            var expectedType = expression.GetType().GetGenericArguments()[0];
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.IsType(expectedType, result);
            Assert.Equal("Hello5", result);
        }

        [Fact]
        public void Append_Closure()
        {
            var world = " World!";
            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name + world);

            var data = @"{
  ""data"":{
    ""simple"":{
      ""name"": ""Hello"",
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
        public void Call_Method()
        {
            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => Greet(x.Name));

            var data = @"{
  ""data"":{
    ""simple"":{
      ""name"": ""World!"",
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
        public void Append_Two_Members()
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

        [Fact]
        public void Append_Two_Identical_Members()
        {
            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name + x.Name);

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
            Assert.Equal("HelloHello", result);
        }

        private static string Greet(string s)
        {
            return "Hello " + s;
        }
    }
}
