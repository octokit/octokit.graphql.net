using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests.Models;
using Octokit.GraphQL.Core.Utilities;
using Newtonsoft.Json.Linq;
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
                QueryableValueExtensions.RewrittenSelect(data["data"]["simple"], x => x["name"]).ToObject<string>();

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
                ExpressionMethods.SelectEntity(data["data"]["simple"], x => new
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
                .Data
                .Select(x => x.Id);

            Expression<Func<JObject, IEnumerable<string>>> expected = data =>
                ExpressionMethods.SelectEntity(data["data"]["data"], x => x["id"].ToObject<string>());

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
                ExpressionMethods.SelectEntity(data["data"]["nested"]["simple"], x => new
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
                .Data
                .Select(x => new
                {
                    x.Id,
                    Items = x.Items.Select(i => i.Name),
                });

            Expression<Func<JObject, object>> expected = data =>
                ExpressionMethods.SelectEntity(
                    data["data"]["data"],
                    x => new
                    {
                        Id = x["id"].ToObject<string>(),
                        Items = ExpressionMethods.SelectEntity(x["items"], i => i["name"].ToObject<string>())
                    });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void Inline_Fragment()
        {
            var expression = new TestQuery()
                .Data
                .OfType<NestedData>()
                .Select(x => new
                {
                    x.Id,
                    Items = x.Items.Select(i => i.Name),
                });

            Expression<Func<JObject, object>> expected = data =>
                ExpressionMethods.Select(
                    ExpressionMethods.ChildrenOfType(data["data"]["data"], "NestedData"),
                    x => new
                    {
                        Id = x["id"].ToObject<string>(),
                        Items = ExpressionMethods.SelectEntity(x["items"], i => i["name"].ToObject<string>())
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
                ExpressionMethods.Select(
                    ((Func<JToken, IQueryable<JToken>>)(x => ExpressionMethods.ChildrenOfType(x, "Simple")))((data["data"]["union"])),
                    x => new
                    {
                        Name = x["name"].ToObject<string>(),
                        Description = x["description"].ToObject<string>(),
                    });

            // We need to remove the (Func<JToken, IQueryable<JToken>>) cast that is needed by
            // C# but not by expression trees.
            expected = (Expression<Func<JObject, object>>)RemoveConvert.Default.Visit(expected);

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        class RemoveConvert : ExpressionVisitor
        {
            private RemoveConvert() { }
            public static readonly RemoveConvert Default = new RemoveConvert();

            protected override Expression VisitBinary(BinaryExpression node)
            {
                return base.VisitBinary(node);
            }

            protected override Expression VisitUnary(UnaryExpression node)
            {
                return Visit(node.NodeType == ExpressionType.Convert ? node.Operand : node);
            }
        }
    }
}
