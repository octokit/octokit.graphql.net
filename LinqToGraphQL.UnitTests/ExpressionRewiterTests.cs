using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL.Builders;
using LinqToGraphQL.UnitTests.Models;
using LinqToGraphQL.Utilities;
using Newtonsoft.Json.Linq;
using Xunit;

namespace LinqToGraphQL.UnitTests
{
    public class ExpressionRewiterTests
    {
        [Fact]
        public void SimpleQuery_Select_Single_Member()
        {
            var expression = new RootQuery()
                .Simple("foo")
                .Select(x => x.Name);

            Expression<Func<JObject, IEnumerable<string>>> expected = data =>
                JsonUtilities.Select(data["data"]["simple"], x => x["name"].ToObject<string>());

            var operation = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), operation.Expression.ToString());
        }

        [Fact]
        public void SimpleQuery_Select_Multiple_Members()
        {
            var expression = new RootQuery()
                .Simple("foo", 2)
                .Select(x => new { x.Name, x.Description });

            Expression<Func<JObject, object>> expected = data =>
                JsonUtilities.Select(data["data"]["simple"], x => new
                {
                    Name = x["name"].ToObject<string>(),
                    Description = x["description"].ToObject<string>(),
                });

            var operation = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), operation.Expression.ToString());
        }

        [Fact]
        public void NestedQuery_Select_Multiple_Members()
        {
            var expression = new RootQuery()
                .Nested("foo")
                .Simple("bar")
                .Select(x => new { x.Name, x.Description });

            Expression<Func<JObject, object>> expected = data =>
                JsonUtilities.Select(data["data"]["nested"]["simple"], x => new
                {
                    Name = x["name"].ToObject<string>(),
                    Description = x["description"].ToObject<string>(),
                });

            var operation = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), operation.Expression.ToString());
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

            Expression<Func<JObject, object>> expected = data =>
                JsonUtilities.Select(
                    data["data"]["data"],
                    x => new
                    {
                        Id = x["id"].ToObject<string>(),
                        Items = JsonUtilities.Select(x["items"], i => i["name"].ToObject<string>())
                    });

            var operation = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), operation.Expression.ToString());
        }
    }
}
