using System;
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

            var query = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name);

            var result = new QuerySerializer().Serialize(new QueryBuilder().Build(query.Expression));

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SimpleQuery_Select_Multiple_Members()
        {
            var expected = "query RootQuery{simple(arg1:\"foo\",arg2:2){name description}}";

            var query = new RootQuery()
                .Simple("foo", 2)
                .Select(x => new { x.Name, x.Description });

            var result = new QuerySerializer().Serialize(new QueryBuilder().Build(query.Expression));

            Assert.Equal(expected, result);
        }

        [Fact]
        public void NestedQuery_Select_Multiple_Members()
        {
            var expected = "query RootQuery{nested(arg1:\"foo\"){simple(arg1:\"bar\"){name description}}}";

            var query = new RootQuery()
                .Nested("foo")
                .Simple("bar")
                .Select(x => new { x.Name, x.Description });

            var result = new QuerySerializer().Serialize(new QueryBuilder().Build(query.Expression));

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Nested_Data()
        {
            var expected = "query RootQuery{data{id items{name}}}";

            var query = new RootQuery()
                .Data
                .Select(x => new
                {
                    x.Id,
                    Items = x.Items.Select(i => i.Name),
                });

            var result = new QuerySerializer().Serialize(new QueryBuilder().Build(query.Expression));

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Inline_Fragment()
        {
            var expected = "query RootQuery{data{... on NestedData{id items{name}}}}";

            var query = new RootQuery()
                .Data
                .OfType<NestedData>()
                .Select(x => new
                {
                    x.Id,
                    Items = x.Items.Select(i => i.Name),
                });

            var result = new QuerySerializer().Serialize(new QueryBuilder().Build(query.Expression));

            Assert.Equal(expected, result);
        }
    }
}
