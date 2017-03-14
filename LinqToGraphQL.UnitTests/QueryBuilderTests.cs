using System.Linq;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Serializers;
using LinqToGraphQL.UnitTests.Models;
using Xunit;

namespace LinqToGraphQL.UnitTests
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
            var expected = "{data{... on NestedData{id items{name}}}}";

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
        public void Boolean_Paramter()
        {
            var expected = "{another(boolean:false){name}}";

            var expression = new RootQuery()
                .Another(false)
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(query.OperationDefinition);

            Assert.Equal(expected, result);
        }
    }
}
