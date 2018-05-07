using System.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.Deserializers;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class PartialEvaluationTests
    {
        [Fact]
        public void Append_String()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name + " World!");

            var data = @"{
  ""data"":{
    ""repository"":{
      ""name"": ""Hello""
    }
  }
}";

            var query = (CompiledQuery<string>)new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal("Hello World!", result);
        }

        [Fact]
        public void Append_Number()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name + x.ForkCount);

            var data = @"{
  ""data"":{
    ""repository"":{
      ""name"": ""Hello"",
      ""forkCount"": 5
    }
  }
}";

            var query = (CompiledQuery<string>)new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal("Hello5", result);
        }

        [Fact]
        public void Append_Closure()
        {
            var world = " World!";
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name + world);

            var data = @"{
  ""data"":{
    ""repository"":{
      ""name"": ""Hello"",
    }
  }
}";

            var query = (CompiledQuery<string>)new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal("Hello World!", result);
        }

        [Fact]
        public void Call_Method()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => Greet(x.Name));

            var data = @"{
  ""data"":{
    ""repository"":{
      ""name"": ""World!"",
    }
  }
}";

            var query = (CompiledQuery<string>)new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal("Hello World!", result);
        }


        [Fact]
        public void Append_Method_Call()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name + Greet("World!"));

            var data = @"{
  ""data"":{
    ""repository"":{
      ""name"": ""Wow! "",
    }
  }
}";

            var query = (CompiledQuery<string>)new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal("Wow! Hello World!", result);
        }


        [Fact]
        public void Append_Two_Members()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name + x.Description);

            var data = @"{
  ""data"":{
    ""repository"":{
      ""name"": ""Hello"",
      ""description"": "" World!""
    }
  }
}";

            var query = (CompiledQuery<string>)new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal("Hello World!", result);
        }

        [Fact]
        public void Append_Two_Identical_Members()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name + x.Name);

            var data = @"{
  ""data"":{
    ""repository"":{
      ""name"": ""Hello"",
      ""description"": "" World!""
    }
  }
}";

            var query = (CompiledQuery<string>)new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal("HelloHello", result);
        }

        [Fact]
        public void Cast_Value()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => (float)x.ForkCount);

            var data = @"{
  ""data"":{
    ""repository"":{
      ""forkCount"": ""12"",
    }
  }
}";

            var query = (CompiledQuery<float>)new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.IsType(typeof(float), result);
            Assert.Equal(12, result);
        }

        [Fact]
        public void Select_String_Length()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name.Length);

            var data = @"{
  ""data"":{
    ""repository"":{
      ""name"": ""Hello World!"",
    }
  }
}";

            var query = (CompiledQuery<int>)new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal(12, result);
        }

        private static string Greet(string s)
        {
            return "Hello " + s;
        }
    }
}
