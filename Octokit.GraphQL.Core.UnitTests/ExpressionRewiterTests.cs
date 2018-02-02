using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class ExpressionRewiterTests
    {
        [Fact]
        public void SimpleQuery_Select_Single_Member()
        {
            var expression = new TestQuery()
                .Simple("foo")
                .Select(x => x.Name);

            Expression<Func<JObject, string>> expected = data =>
                Rewritten.Value.Select(data["data"]["simple"], x => x["name"]).ToObject<string>();

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void SimpleQuery_Select_Multiple_Members()
        {
            var expression = new TestQuery()
                .Simple("foo", 2)
                .Select(x => new { x.Name, x.Description });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(data["data"]["simple"], x => new
                {
                    Name = x["name"].ToObject<string>(),
                    Description = x["description"].ToObject<string>(),
                });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void Data_Select_Single_Member()
        {
            var expression = new TestQuery()
                .QueryItems
                .Select(x => x.Id);

            Expression<Func<JObject, IEnumerable<string>>> expected = data =>
                Rewritten.List.Select(data["data"]["queryItems"], x => x["id"].ToObject<string>());

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void NestedQuery_Select_Multiple_Members()
        {
            var expression = new TestQuery()
                .Nested("foo")
                .Simple("bar")
                .Select(x => new { x.Name, x.Description });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(data["data"]["nested"]["simple"], x => new
                {
                    Name = x["name"].ToObject<string>(),
                    Description = x["description"].ToObject<string>(),
                });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void Nested_Selects()
        {
            var expression = new TestQuery()
                .QueryItems
                .Select(x => new
                {
                    x.Id,
                    Items = x.NestedItems.Select(i => i.Name).ToList(),
                });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.List.Select(
                    data["data"]["queryItems"],
                    x => new
                    {
                        Id = x["id"].ToObject<string>(),
                        Items = Rewritten.List.ToList<string>(Rewritten.List.Select(x["nestedItems"], i => i["name"]))
                    });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void Nested_Select_Value_Single()
        {
            var expression = new TestQuery()
                .Nested("foo")
                .Select(x => new
                {
                    Value = x.Simple("bar").Select(y => new
                    {
                        y.Name,
                        y.Number,
                    }).Single()
                });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.List.Select(
                    data["data"]["nested"],
                    x => new
                    {
                        Value = Rewritten.Value.Single(
                            Rewritten.Value.Select(
                                x["simple"],
                                y => new
                                {
                                    Name = y["name"].ToObject<string>(),
                                    Number = y["number"].ToObject<int>(),
                                }))
                    });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void Nested_Select_Value_SingleOrDefault()
        {
            var expression = new TestQuery()
                .Nested("foo")
                .Select(x => new
                {
                    Value = x.Simple("bar").Select(y => new
                    {
                        y.Name,
                        y.Number,
                    }).SingleOrDefault()
                });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.List.Select(
                    data["data"]["nested"],
                    x => new
                    {
                        Value = Rewritten.Value.SingleOrDefault(
                            Rewritten.Value.Select(
                                x["simple"],
                                y => new
                                {
                                    Name = y["name"].ToObject<string>(),
                                    Number = y["number"].ToObject<int>(),
                                }))
                    });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void Inline_Fragment()
        {
            var expression = new TestQuery()
                .QueryItems
                .OfType<NestedData>()
                .Select(x => new
                {
                    x.Id,
                    Items = x.NestedItems.Select(i => i.Name).ToList(),
                });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.List.Select(
                    Rewritten.List.OfType(data["data"]["queryItems"], "NestedData"),
                    x => new
                    {
                        Id = x["id"].ToObject<string>(),
                        Items = Rewritten.List.ToList<string>(Rewritten.List.Select(x["nestedItems"], i => i["name"]))
                    });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }


        [Fact]
        public void Union()
        {
            var expression = new TestQuery()
                .Union
                .Select(x => x.Simple)
                .Select(x => new
                {
                    x.Name,
                    x.Description,
                });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(
                    Rewritten.Value.Select(data["data"]["union"], x => Rewritten.Value.OfType(x, "Simple")),
                    x => new
                    {
                        Name = x["name"].ToObject<string>(),
                        Description = x["description"].ToObject<string>(),
                    });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void Interface_Cast()
        {
            var expression = new TestQuery()
                .Node(123)
                .Cast<Simple>()
                .Select(x => x.Name);

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(
                    Rewritten.Interface.Cast(data["data"]["node"], "Simple"),
                    x => x["name"]).ToObject<string>();

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }
    }
}
