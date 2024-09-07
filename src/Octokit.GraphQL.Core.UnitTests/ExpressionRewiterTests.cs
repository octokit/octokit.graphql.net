using System;
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
            var query = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name);

            Expression<Func<JObject, string>> expected = data =>
                Rewritten.Value.SelectJToken(data["data"]["repository"], x => x["name"]).ToObject<string>();

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Repository_Select_Multiple_Members()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .Select(x => new { x.Name, x.Description });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(data["data"]["repository"], x => new
                {
                    Name = x["name"].ToObject<string>(),
                    Description = x["description"].ToObject<string>(),
                });

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Repository_Issues_Select_Multiple_Members()
        {
            var query = new Query()
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

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Licence_Conditions_Nested_Selects()
        {
            var query = new Query()
                .Licenses
                .Select(x => new
                {
                    x.Body,
                    Items = x.Conditions.Select(i => i.Description).ToList(),
                });

            Expression<Func<JObject, object>> expected = data =>
                (IEnumerable<object>)Rewritten.List.Select(
                    data["data"]["licenses"],
                    x => new
                    {
                        Body = x["body"].ToObject<string>(),
                        Items = Rewritten.List.ToList<string>(Rewritten.List.Select(x["items"], i => i["description"]))
                    }).ToList();

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Licenses_Conditions_Select_ToDictionary()
        {
            var query = new Query()
                .Licenses
                .Select(x => new
                {
                    x.Body,
                    Items = x.Conditions.Select(i => new
                    {
                        i.Key,
                        i.Description,
                    }).ToDictionary(d => d.Key, d => d.Description),
                });

            Expression<Func<JObject, object>> expected = data =>
                (IEnumerable<object>) Rewritten.List.Select(
                    data["data"]["licenses"],
                    x => new
                    {
                        Body = x["body"].ToObject<string>(),
                        Items = (IDictionary<string, string>) Rewritten.List.Select(x["items"], i => new
                        {
                            Key = i["key"].ToObject<string>(),
                            Description = i["description"].ToObject<string>()
                        }).ToDictionary(d => d.Key, d => d.Description)
                    }).ToList();

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Issue_Milestone_Select_Value_Single()
        {
            var query = new Query()
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
                Rewritten.Value.Select(
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

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Issue_Milestone_Select_Value_SingleOrDefault()
        {
            var query = new Query()
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
                Rewritten.Value.Select(
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

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Nodes_OfType()
        {
            var query = new Query()
                .Nodes(new[] { new ID("123") })
                .OfType<Issue>()
                .Select(x => new
                {
                    x.Body,
                });

            Expression<Func<JObject, object>> expected = data =>
                (IEnumerable<object>)Rewritten.List.Select(
                    Rewritten.List.OfType(data["data"]["nodes"], "Issue"),
                    x => new
                    {
                        Body = x["body"].ToObject<string>(),
                    }).ToList();

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Node_OfType()
        {
            var query = new Query()
                .Node(new ID("123"))
                .Cast<Issue>()
                .Select(x => x.Body);

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.SelectJToken(
                    Rewritten.Interface.Cast(data["data"]["node"], "Issue"),
                    x => x["body"]).ToObject<string>();

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Can_Use_Conditional_With_Null_Result()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .Select(x => !string.IsNullOrWhiteSpace(x.Name) ? x.Name : null);

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(
                    data["data"]["repository"],
                    x => !string.IsNullOrWhiteSpace(x["name"].ToObject<string>()) ? x["name"].ToObject<string>() : null);

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Can_Use_Conditional_To_Compare_To_Null()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .Select(x => x.Name != null ? x.Name : null);

            // Expression<Func<JObject, object>> expected = data =>
            //     Rewritten.Value.Select(
            //         data["data"]["repository"],
            //         x => x["name"].Type != JTokenType.Null ? x["name"].ToObject<string>() : null);

            var readableString = 
                "data => Rewritten.Value.Select(data[\"data\"][\"repository\"],x => (x[\"name\"].Type != JTokenType.Null) ? x[\"name\"].ToObject<string>() : null)";
            
            // Expression put through ReadableExpression outputs the following, so I'm using a hard coded string instead
            //   data => Rewritten.Value.Select(data["data"]["repository"], x => (((int)x["name"].Type) != 10) ? x["name"].ToObject<string>() : null)

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(readableString, query);
        }

        
        [Fact]
        public void Union_IssueOrPullRequest()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .IssueOrPullRequest(1)
                .Select(issueOrPr => issueOrPr.Switch<object>(when =>
                    when.Issue(issue => new IssueModel
                    {
                        Number = issue.Number,
                    }).PullRequest(pr => new PullRequestModel
                    {
                        Title = pr.Title,
                    })));

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(
                    data["data"]["repository"]["issueOrPullRequest"],
                    issueOrPr =>  Rewritten.Value.Switch(
                        issueOrPr,
                        new Dictionary<string, Func<JToken, object>>
                        {
                            { "Issue", issue => new IssueModel { Number = issue["number"].ToObject<int>() } },
                            { "PullRequest", pr => new PullRequestModel { Title = pr["title"].ToObject<string>() } },
                        }));

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Union_PullRequest_Timeline()
        {
            var query = new Query()
                .Repository("foo", "bar")
                .PullRequest(1)
                .Timeline(first: 100)
                .Nodes
                .Select(node => node.Switch<TimelineItemModel>(when =>
                    when.Commit(commit => new CommitModel
                    {
                        Oid = commit.AbbreviatedOid,
                    }).IssueComment(comment => new IssueCommentModel
                    {
                        Body = comment.Body,
                    })));

            Expression<Func<JObject, IEnumerable<TimelineItemModel>>> expected = data =>
                (IEnumerable<TimelineItemModel>)Rewritten.List.Select(
                    data["data"]["repository"]["pullRequest"]["timeline"]["nodes"],
                    node => Rewritten.Value.Switch(
                        node,
                        new Dictionary<string, Func<JToken, TimelineItemModel>>
                        {
                            { "Commit", commit => new CommitModel { Oid = commit["oid"].ToObject<string>() } },
                            { "IssueComment", comment => new IssueCommentModel { Body = comment["body"].ToObject<string>() } },
                        })).ToList();

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        class IssueModel
        {
            public int Number { get; set; }
        }

        class PullRequestModel
        {
            public string Title { get; set; }
        }

        class TimelineItemModel
        {
        }

        class CommitModel : TimelineItemModel
        {
            public string Oid { get; set; }
        }

        class IssueCommentModel : TimelineItemModel
        {
            public string Body { get; set; }
        }
    }
}
