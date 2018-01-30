using System;
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
            var expected = "query{simple(arg1:\"foo\"){name}}";

            var expression = new TestQuery()
                .Simple("foo")
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Select_Multiple_Members()
        {
            var expected = "query{simple(arg1:\"foo\",arg2:2){name description}}";

            var expression = new TestQuery()
                .Simple("foo", 2)
                .Select(x => new { x.Name, x.Description });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Select_Single_Member_Append_String()
        {
            var expected = "query{simple(arg1:\"foo\"){name}}";

            var expression = new TestQuery()
                .Simple("foo")
                .Select(x => x.Name + " World!");

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Select_Append_Two_Members()
        {
            var expected = "query{simple(arg1:\"foo\"){name description}}";

            var expression = new TestQuery()
                .Simple("foo")
                .Select(x => x.Name + x.Description);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Select_Append_Two_Identical_Members()
        {
            var expected = "query{simple(arg1:\"foo\"){name}}";

            var expression = new TestQuery()
                .Simple("foo")
                .Select(x => x.Name + x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Cast_Member_To_Enum()
        {
            var expected = "query{simple(arg1:\"foo\"){number}}";

            var expression = new TestQuery()
                .Simple("foo")
                .Select(x => new
                {
                    Float = (DayOfWeek)x.Number
                });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Cast_Nullable_Member_To_Enum()
        {
            var expected = "query{simple(arg1:\"foo\"){nullableNumber}}";

            var expression = new TestQuery()
                .Simple("foo")
                .Select(x => new
                {
                    Float = (DayOfWeek)x.NullableNumber.Value
                });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Data_Select_Single_Member()
        {
            var expected = "query{queryItems{id}}";

            var expression = new TestQuery()
                .QueryItems
                .Select(x => x.Id);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Data_Select_Multiple_Members()
        {
            var expected = "query{queryItems{id name}}";

            var expression = new TestQuery()
                .QueryItems
                .Select(x => new{ x.Id, x.Name});

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void NestedQuery_Select_Multiple_Members()
        {
            var expected = "query{nested(arg1:\"foo\"){simple(arg1:\"bar\"){name description}}}";

            var expression = new TestQuery()
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
            var expected = "query{queryItems{id nestedItems{name}}}";

            var expression = new TestQuery()
                .QueryItems
                .Select(x => new
                {
                    x.Id,
                    Items = x.NestedItems.Select(i => i.Name).ToList(),
                });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Inline_Fragment()
        {
            var expected = "query{queryItems{... on NestedData{__typename id nestedItems{name}}}}";

            var expression = new TestQuery()
                .QueryItems
                .OfType<NestedData>()
                .Select(x => new
                {
                    x.Id,
                    Items = x.NestedItems.Select(i => i.Name).ToList(),
                });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Field_Aliases()
        {
            var expected = @"query {
    simple(arg1: ""foo"", arg2: 1) {
        foo: name
        bar: description
    }
}";

            var expression = new TestQuery()
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
            var expected = "query{boolValue(boolean:false){name}}";

            var expression = new TestQuery()
                .BoolValue(false)
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Enumerable_Parameter()
        {
            var expected = "query{enumerableValue(value:[{value1:\"Hello World\",value2:42},{value1:\"Goodbye World\",value2:24}]){name}}";

            var expression = new TestQuery()
                .EnumerableValue(new[]
                {
                    new SampleObject()
                    {
                        Value1 = "Hello World",
                        Value2 = 42
                    },
                    new SampleObject()
                    {
                        Value1 = "Goodbye World",
                        Value2 = 24
                    },
                })
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void String_Parameter()
        {
            var expected = "query{stringValue(value:\"hello\"){name}}";

            var expression = new TestQuery()
                .StringValue("hello")
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Integer_Parameter()
        {
            var expected = "query{intValue(integer:123){name}}";

            var expression = new TestQuery()
                .IntValue(123)
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Float_Parameter()
        {
            var expected = "query{floatValue(flt:123.3){name}}";

            var expression = new TestQuery()
                .FloatValue(123.3f)
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Object_Null_Parameter()
        {
            var expected = "query{objectValue{name}}";

            var expression = new TestQuery()
                .ObjectValue(null)
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void InputObject_Parameter()
        {
            var expected = "query{inputObject(input:{stringValue:\"foo\"}){name}}";

            var input = new InputObject
            {
                StringValue = "foo",
            };

            var expression = new TestQuery()
                .InputObject(input)
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void InputObject_Parameter_With_Null_Field()
        {
            var expected = "query{inputObject(input:{stringValue:null}){name}}";

            var input = new InputObject
            {
                StringValue = null,
            };

            var expression = new TestQuery()
                .InputObject(input)
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
            var expected = "query{objectValue(value:{value1:\"Hello World\",value2:42}){name}}";

            var expression = new TestQuery()
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
            var expected = "query{objectValue(value:{value1:\"Hello World\",value2:42}){name}}";

            var expression = new TestQuery()
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

            expected = "query{objectValue(value:{value1:\"A different answer\",value2:14}){name}}";

            expression = new TestQuery()
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
            var expected = @"query {
    union {
        ... on Simple {
            __typename
            name
            description
        }
    }
}";

            var expression = new TestQuery()
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

        [Fact]
        public void Union_Select_Child_Property()
        {
            var expected = @"query {
    union {
        ... on Simple {
            __typename
            name
        }
    }
}";

            var expression = new TestQuery()
                .Union
                .Select(x => x.Simple.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer(4).Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Union_Select_Child_Property_To_Class()
        {
            var expected = @"query {
    union {
        ... on Simple {
            __typename
            name
        }
    }
}";

            var expression = new TestQuery()
                .Union
                .Select((Union x) => new
                {
                    x.Simple.Name,
                });

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer(4).Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }
    }
}
