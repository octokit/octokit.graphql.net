using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests.Models;
using Xunit;

namespace Octokit.GraphQL.Core.UnitTests
{
    public class ExpressionRewiterTests
    {
        public ExpressionRewiterTests()
        {
            ExpressionCompiler.IsUnitTesting = true;
        }

        [Fact]
        public void Repository_Select_Single_Member()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name);

            Expression<Func<JObject, string>> expected = data =>
                Rewritten.Value.Select(data["data"]["repository"], x => x["name"]).ToObject<string>();

            var query = expression.Compile();
            Assert.Equal(expected.ToString(), query.GetResultBuilderExpression().ToString());
        }

        [Fact]
        public void Repository_Select_Multiple_Members()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => new { x.Name, x.Description });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(data["data"]["repository"], x => new
                {
                    Name = x["name"].ToObject<string>(),
                    Description = x["description"].ToObject<string>(),
                });

            var query = expression.Compile();
            Assert.Equal(expected.ToString(), query.GetResultBuilderExpression().ToString());
        }

        [Fact]
        public void Repository_Issues_Select_Multiple_Members()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Issues(10)
                .Nodes
                .Select(x => new { x.Body, x.Closed });

            Expression<Func<JObject, IEnumerable<object>>> expected = data =>
                (IEnumerable<object>)Rewritten.List.Select(data["data"]["repository"]["issues"]["nodes"], x => new
                {
                    Body = x["body"].ToObject<string>(),
                    Closed = x["closed"].ToObject<bool>(),
                }).ToList();

            var query = expression.Compile();
            Assert.Equal(expected.ToString(), query.GetResultBuilderExpression().ToString());
        }

        [Fact]
        public void Licence_Conditions_Nested_Selects()
        {
            var expression = new Query()
                .Licenses
                .Select(x => new
                {
                    x.Body,
                    Items = x.Conditions.Select(i => i.Description).ToList(),
                });

            Expression<Func<JObject, object>> expected = data =>
                (IEnumerable)Rewritten.List.Select(
                    data["data"]["licenses"],
                    x => new
                    {
                        Body = x["body"].ToObject<string>(),
                        Items = Rewritten.List.ToList<string>(Rewritten.List.Select(x["items"], i => i["description"]))
                    }).ToList();

            var query = expression.Compile();
            Assert.Equal(expected.ToString(), query.GetResultBuilderExpression().ToString());
        }

        [Fact]
        public void Issue_Milestone_Select_Value_Single()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Issue(1)
                .Select(x => new
                {
                    Value = x.Milestone.Select(y => new
                    {
                        y.Closed,
                        y.Description,
                    }).Single()
                });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.List.Select(
                    data["data"]["repository"]["issue"],
                    x => new
                    {
                        Value = Rewritten.Value.Single(
                            Rewritten.Value.Select(
                                x["value"],
                                y => new
                                {
                                    Closed = y["closed"].ToObject<bool>(),
                                    Description = y["description"].ToObject<string>(),
                                }))
                    });

            var query = expression.Compile();
            Assert.Equal(expected.ToString(), query.GetResultBuilderExpression().ToString());
        }

        [Fact]
        public void Issue_Milestone_Select_Value_SingleOrDefault()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Issue(1)
                .Select(x => new
                {
                    Value = x.Milestone.Select(y => new
                    {
                        y.Closed,
                        y.Description,
                    }).SingleOrDefault()
                });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.List.Select(
                    data["data"]["repository"]["issue"],
                    x => new
                    {
                        Value = Rewritten.Value.SingleOrDefault(
                            Rewritten.Value.Select(
                                x["value"],
                                y => new
                                {
                                    Closed = y["closed"].ToObject<bool>(),
                                    Description = y["description"].ToObject<string>(),
                                }))
                    });

            var query = expression.Compile();
            Assert.Equal(expected.ToString(), query.GetResultBuilderExpression().ToString());
        }

        [Fact]
        public void Nodes_OfType()
        {
            var expression = new Query()
                .Nodes(new[] { new ID("123") })
                .OfType<Issue>()
                .Select(x => new
                {
                    x.Body,
                });

            Expression<Func<JObject, object>> expected = data =>
                (IEnumerable)Rewritten.List.Select(
                    Rewritten.List.OfType(data["data"]["nodes"], "Issue"),
                    x => new
                    {
                        Body = x["body"].ToObject<string>(),
                    }).ToList();

            var query = expression.Compile();
            Assert.Equal(expected.ToString(), query.GetResultBuilderExpression().ToString());
        }

        [Fact]
        public void Node_OfType()
        {
            var expression = new Query()
                .Node(new ID("123"))
                .Cast<Issue>()
                .Select(x => x.Body);

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(
                    Rewritten.Interface.Cast(data["data"]["node"], "Issue"),
                    x => x["body"]).ToObject<string>();

            var query = expression.Compile();
            Assert.Equal(expected.ToString(), query.GetResultBuilderExpression().ToString());
        }

        [Fact]
        public void Can_Use_Conditional_With_Null_Result()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => !string.IsNullOrWhiteSpace(x.Name) ? x.Name : null);

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(
                    data["data"]["repository"],
                    x => !string.IsNullOrWhiteSpace(x["name"].ToObject<string>()) ? x["name"].ToObject<string>() : null);

            var query = expression.Compile();
            Assert.Equal(expected.ToString(), query.GetResultBuilderExpression().ToString());
        }

        [Fact]
        public void Can_Use_Conditional_To_Compare_To_Null()
        {
            var expression = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name != null ? x.Name : null);

            // C# inserts a Convert() around the comparison in the following expression, making the test fail.
            //
            // Expression<Func<JObject, object>> expected = data =>
            //     Rewritten.Value.Select(
            //         data["data"]["repository"],
            //         x => x["name"].Type != JTokenType.Null ? x["name"] : null).ToObject<string>();
            //
            // Just hardcode the expected output.
            var expected = "data => Select(data.get_Item(\"data\").get_Item(\"repository\"), x => IIF((x.get_Item(\"name\").Type != Null), x.get_Item(\"name\").ToObject(), null))";

            var query = expression.Compile();
            Assert.Equal(expected, query.GetResultBuilderExpression().ToString());
        }
    }
}
