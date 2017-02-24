using System.Linq;
using LinqToGraphQL.Builders;
using LinqToGraphQL.UnitTests.Models;
using Xunit;

namespace LinqToGraphQL.UnitTests
{
    public class QueryBuilderTests
    {
        [Fact]
        public void SimpleQuery_Select_Single_Member()
        {
            var expected = "query RootQuery{simple(arg1:\"foo\"){name}}";

            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name);

            var query = new QueryBuilder().Build(expression);
            var operation = query.OperationDefinition;
            var deserialized = new QuerySerializer().Serialize(operation);

            Assert.Equal(expected, deserialized);
        }

        [Fact]
        public void SimpleQuery_Select_Multiple_Members()
        {
            var expected = "query RootQuery{simple(arg1:\"foo\",arg2:2){name description}}";

            var expression = new RootQuery()
                .Simple("foo", 2)
                .Select(x => new { x.Name, x.Description });

            var query = new QueryBuilder().Build(expression);
            var operation = query.OperationDefinition;
            var deserialized = new QuerySerializer().Serialize(operation);

            Assert.Equal(expected, deserialized);
        }

        [Fact]
        public void Data_Select_Single_Member()
        {
            var expected = "query RootQuery{data{id}}";

            var expression = new RootQuery()
                .Data
                .Select(x => x.Id);

            var query = new QueryBuilder().Build(expression);
            var operation = query.OperationDefinition;
            var deserialized = new QuerySerializer().Serialize(operation);

            Assert.Equal(expected, deserialized);
        }

        [Fact]
        public void NestedQuery_Select_Multiple_Members()
        {
            var expected = "query RootQuery{nested(arg1:\"foo\"){simple(arg1:\"bar\"){name description}}}";

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
            var expected = "query RootQuery{data{id items{name}}}";

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
            var expected = "query RootQuery{data{... on NestedData{id items{name}}}}";

            var expression = new RootQuery()
                .Data
                .OfType<NestedData>()
                .Select(x => new
                {
                    x.Id,
                    Items = x.Items.Select(i => i.Name),
                });

            var operation = new QueryBuilder().Build(expression);
            var result = new QuerySerializer().Serialize(operation.OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Field_Aliases()
        {
            var expected = @"query RootQuery {
    foo: simple(arg1: ""foo"", arg2: 1) {
        name
    }
    bar: simple(arg1: ""bar"", arg2: 2) {
        name
    }
}";

            var expression = new RootQuery()
                .Select(x => new
                {
                    Foo = x.Simple("foo", 1).Select(i => i.Name),
                    Bar = x.Simple("bar", 2).Select(i => i.Name),
                });

            var operation = new QueryBuilder().Build(expression);
            var result = new QuerySerializer(4).Serialize(operation.OperationDefinition);

            Assert.Equal(expected, result);
        }
    }
}
