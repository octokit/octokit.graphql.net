using System.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.Serializers;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class QueryBuilderTests
    {
        [Fact]
        public void SimpleQuery_Select_Single_Member()
        {
            var expected = "{simple(arg1:\"foo\"){name}}";

            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Select_Multiple_Members()
        {
            var expected = "{simple(arg1:\"foo\",arg2:2){name description}}";

            var expression = new RootQuery()
                .Simple("foo", 2)
                .Select(x => new { x.Name, x.Description });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Select_Single_Member_Append_String()
        {
            var expected = "{simple(arg1:\"foo\"){name}}";

            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name + " World!");

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Select_Append_Two_Members()
        {
            var expected = "{simple(arg1:\"foo\"){name description}}";

            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name + x.Description);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Select_Append_Two_Identical_Members()
        {
            var expected = "{simple(arg1:\"foo\"){name}}";

            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name + x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Where_Error()
        {
            var expected = "{simple(arg1:\"foo\"){name}}";

            var expression = new RootQuery()
                .Simple("foo")
                .Where(simple => simple.Name == "Something")
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
        }

        [Fact]
        public void SimpleQuery_GroupBy_Error()
        {
            var expected = "{simple(arg1:\"foo\"){name}}";

            var expression = new RootQuery()
                .Simple("foo")
                .GroupBy(simple => simple.Name);

            var query = new QueryBuilder().Build(expression);
        }

        [Fact]
        public void Data_Select_Single_Member()
        {
            var expected = "{data{id}}";

            var expression = new RootQuery()
                .Data
                .Select(x => x.Id);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void NestedQuery_Select_Multiple_Members()
        {
            var expected = "{nested(arg1:\"foo\"){simple(arg1:\"bar\"){name description}}}";

            var expression = new RootQuery()
                .Nested("foo")
                .Simple("bar")
                .Select(x => new { x.Name, x.Description });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Nested_Selects()
        {
            var expected = "{data{id items{name}}}";

            var expression = new RootQuery()
                .Data
                .Select(x => new
                {
                    x.Id,
                    Items = x.Items.Select(i => i.Name),
                });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Inline_Fragment()
        {
            var expected = "{data{... on NestedData{__typename id items{name}}}}";

            var expression = new RootQuery()
                .Data
                .OfType<NestedData>()
                .Select(x => new
                {
                    x.Id,
                    Items = x.Items.Select(i => i.Name),
                });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Field_Aliases()
        {
            var expected = @"{
    simple(arg1: ""foo"", arg2: 1) {
        foo: name
        bar: description
    }
}";

            var expression = new RootQuery()
                .Simple("foo", 1)
                .Select(x => new
                {
                    Foo = x.Name,
                    Bar = x.Description,
                });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer(4).Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Boolean_Parameter()
        {
            var expected = "{boolValue(boolean:false){name}}";

            var expression = new RootQuery()
                .BoolValue(false)
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void String_Parameter()
        {
            var expected = "{stringValue(value:\"hello\"){name}}";

            var expression = new RootQuery()
                .StringValue("hello")
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Integer_Parameter()
        {
            var expected = "{intValue(integer:123){name}}";

            var expression = new RootQuery()
                .IntValue(123)
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Float_Parameter()
        {
            var expected = "{floatValue(flt:123.3){name}}";

            var expression = new RootQuery()
                .FloatValue(123.3f)
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Object_Null_Parameter()
        {
            var expected = "{objectValue{name}}";

            var expression = new RootQuery()
                .ObjectValue(null)
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        public class SampleObject
        {
            public string Value1 { get; set; }

            public int Value2 { get; set; }
        }

        [Fact]
        public void Object_Parameter()
        {
            var expected = "{objectValue(value:{value1:\"Hello World\",value2:42}){name}}";

            var expression = new RootQuery()
                .ObjectValue(new SampleObject()
                {
                    Value1 = "Hello World",
                    Value2 = 42
                })
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Object_Parameter_Hit_Cache()
        {
            var expected = "{objectValue(value:{value1:\"Hello World\",value2:42}){name}}";

            var expression = new RootQuery()
                .ObjectValue(new SampleObject()
                {
                    Value1 = "Hello World",
                    Value2 = 42
                })
                .Select(x => x.Name);

            var querySerializer = new QuerySerializer();
            var queryBuilder = new QueryBuilder();

            var query = queryBuilder.Build(expression);
            var result = querySerializer.Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);

            expected = "{objectValue(value:{value1:\"A different answer\",value2:14}){name}}";

            expression = new RootQuery()
                .ObjectValue(new SampleObject()
                {
                    Value1 = "A different answer",
                    Value2 = 14
                })
                .Select(x => x.Name);

            query = queryBuilder.Build(expression);
            result = querySerializer.Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Union()
        {
            var expected = @"{
    union {
        ... on Simple {
            __typename
            name
            description
        }
    }
}";

            var expression = new RootQuery()
                .Union
                .Select(x => x.Simple)
                .Select(x => new
                {
                    x.Name,
                    x.Description,
                });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer(4).Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }
    }
}
