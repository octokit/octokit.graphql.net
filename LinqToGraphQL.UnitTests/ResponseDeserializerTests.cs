using System;
using System.Linq;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Deserializers;
using LinqToGraphQL.UnitTests.Models;
using Newtonsoft.Json.Linq;
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

            var operation = new QueryBuilder().Build(query);
            var expectedType = query.GetType().GetGenericArguments()[0];
            var result = new ResponseDeserializer().Deserialize(operation, data).Single();

            Assert.IsType(expectedType, result);
            Assert.Equal("Hello World!", result);
        }

        [Fact]
        public void SimpleQuery_Select_Multiple_Members()
        {
            var expression = new RootQuery()
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

            var query = new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.Equal("Hello World!", result.Name);
            Assert.Equal("Goodbye cruel world", result.Description);
        }

        [Fact]
        public void Data_Select_Single_Member()
        {
            var expression = new RootQuery()
                .Data
                .Select(x => x.Id);

            var data = @"{
  ""data"":{
    ""data"":[
      { ""id"": ""foo"" },
      { ""id"": ""bar"" }
    ]
  }
}";

            var query = new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal(new[] { "foo", "bar" }, result);
        }

        [Fact]
        public void NestedQuery_Select_Multiple_Members()
        {
            var expression = new RootQuery()
                .Nested("foo")
                .Simple("bar")
                .Select(x => new { x.Name, x.Description });

            var data = @"{
    ""data"":{
        ""nested"": {
            ""simple"":{
            ""name"": ""Hello World!"",
            ""description"": ""Goodbye cruel world""
            }
        }
    }
}";

            var query = new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.Equal("Hello World!", result.Name);
            Assert.Equal("Goodbye cruel world", result.Description);
        }

        [Fact]
        public void NestedQuery_Select_Multiple_Members_As_Ctor_Parameters()
        {
            var expression = new RootQuery()
                .Nested("foo")
                .Simple("bar")
                .Select(x => new NamedClass(x.Name, x.Description));

            var data = @"{
    ""data"":{
        ""nested"": {
            ""simple"":{
            ""name"": ""Hello World!"",
            ""description"": ""Goodbye cruel world""
            }
        }
    }
}";

            var query = new QueryBuilder().Build(expression);
            var expectedType = expression.GetType().GetGenericArguments()[0];
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.IsType(expectedType, result);
            Assert.Equal("Hello World!", result.Name);
            Assert.Equal("Goodbye cruel world", result.Description);
        }

        [Fact]
        public void NestedQuery_Select_Multiple_Members_To_Named_Class()
        {
            var expression = new RootQuery()
                .Nested("foo")
                .Simple("bar")
                .Select(x => new NamedClass
                {
                    Name = x.Name,
                    Description = x.Description,
                });

            var data = @"{
    ""data"":{
        ""nested"": {
            ""simple"":{
            ""name"": ""Hello World!"",
            ""description"": ""Goodbye cruel world""
            }
        }
    }
}";

            var query = new QueryBuilder().Build(expression);
            var expectedType = expression.GetType().GetGenericArguments()[0];
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.IsType(expectedType, result);
            Assert.Equal("Hello World!", result.Name);
            Assert.Equal("Goodbye cruel world", result.Description);
        }

        [Fact]
        public void Nested_Selects()
        {
            var expression = new RootQuery()
                .Data
                .Select(x => new
                {
                    x.Id,
                    Items = x.Items.Select(i => i.Name),
                });

            var data = @"{
    ""data"":{
        ""data"": {
            ""id"": ""foo"",
            ""items"": [
                { ""name"": ""item1"" },
                { ""name"": ""item2"" }
            ]
        }
    }
}";

            var foo = JObject.Parse(data);

            var query = new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.Equal("foo", result.Id);
            Assert.Equal(new[] { "item1", "item2" }, result.Items);
        }

        [Fact]
        public void Select_ToList()
        {
            var expression = new RootQuery()
                .Data
                .Select(x => new
                {
                    x.Id,
                    Items = x.Items.Select(i => i.Name).ToList(),
                });

            var data = @"{
    ""data"":{
        ""data"": {
            ""id"": ""foo"",
            ""items"": [
                { ""name"": ""item1"" },
                { ""name"": ""item2"" }
            ]
        }
    }
}";

            var foo = JObject.Parse(data);

            var query = new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.Equal("foo", result.Id);
            Assert.Equal(new[] { "item1", "item2" }, result.Items);
        }

        private class NamedClass
        {
            public NamedClass()
            {
            }

            public NamedClass(string name, string description)
            {
                Name = name;
                Description = description;
            }

            public string Name { get; set; }
            public string Description { get; set; }
        }
    }
}
