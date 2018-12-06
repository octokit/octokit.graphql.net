using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.UnitTests;
using Octokit.GraphQL.Model;
using Xunit;

namespace Octokit.GraphQL.UnitTests
{
    public class ExpressionRewiterTests
    {
        public ExpressionRewiterTests()
        {
            ExpressionCompiler.IsUnitTesting = true;
        }

        [Fact]
        public void RepositoryOwner_Repository_Query()
        {
            var query = new Query()
                .RepositoryOwner("foo")
                .Repository("bar")
                .Select((Repository x) => new
                {
                    Id = x.Id.ToString(),
                    x.Name,
                    Owner = x.Owner.Select(o => new
                    {
                        o.Login
                    }).Single(),
                    x.IsFork,
                    x.IsPrivate,
                });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(
                    data["data"]["repositoryOwner"]["repository"],
                    x => new
                    {
                        Id = x["id"].ToString(),
                        Name = x["name"].ToObject<string>(),
                        Owner = Rewritten.Value.Single(
                            Rewritten.Value.Select(
                                x["owner"],
                                o => new { Login = o["login"].ToObject<string>() })),
                        IsFork = x["isFork"].ToObject<bool>(),
                        IsPrivate = x["isPrivate"].ToObject<bool>(),
                    });

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Repository_Select_Use_Fragment_Twice()
        {
            var fragment = new Fragment<Repository, string>("repositoryName", repository => repository.Name);
            
            var query = new Query()
                .Select(q => new
                {
                    repo1 = q.Repository("foo", "bar").Select(fragment).SingleOrDefault(),
                    repo2 = q.Repository("foo", "bar").Select(fragment).SingleOrDefault()
                });

            Expression<Func<JObject, object>> expected = data =>
                Rewritten.Value.Select(
                    data["data"],
                    q => new
                    {
                        repo1 = Rewritten.Value.SingleOrDefault(
                            Rewritten.Value.SelectFragment(
                                q["repo1"],
                                repository => repository["name"].ToObject<string>())),
                        repo2 = Rewritten.Value.SingleOrDefault(
                            Rewritten.Value.SelectFragment(
                                q["repo2"],
                                repository => repository["name"].ToObject<string>())),
                    });

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void RepositoryOwner_Repositories_Query()
        {
            var query = new Query()
                .RepositoryOwner(login: "foo")
                .Repositories(first: 30)
                .Edges
                .Select(x => x.Node)
                .Select(x => new
                {
                    Id = x.Id.ToString(),
                    x.Name,
                    Owner = x.Owner.Select(o => new
                    {
                        o.Login
                    }).Single(),
                    x.IsFork,
                    x.IsPrivate,
                });

            Expression<Func<JObject, IEnumerable<object>>> expected = data =>
                (IEnumerable<object>)Rewritten.List.Select(
                    Rewritten.List.Select(
                        data["data"]["repositoryOwner"]["repositories"]["edges"],
                        x => x["node"]),
                        x => new
                        {
                            Id = x["id"].ToString(),
                            Name = x["name"].ToObject<string>(),
                            Owner = Rewritten.Value.Single(
                                Rewritten.Value.Select(
                                    x["owner"],
                                    o => new { Login = o["login"].ToObject<string>() })),
                            IsFork = x["isFork"].ToObject<bool>(),
                            IsPrivate = x["isPrivate"].ToObject<bool>(),
                        }).ToList();

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Repository_Details_With_Viewer()
        {
            var query = new Query()
                .Select(x => x.RepositoryOwner("foo")
                              .Repositories(30, null, null, null, null, null, null, null, null, null)
                              .Edges
                              .Select(y => y.Node)
                              .Select(z => new
                              {
                                  z.Name,
                                  z.IsPrivate,
                                  Viewer = x.Viewer.Select(a => new
                                  {
                                      a.Login
                                  }).Single()
                              }));

            Expression<Func<JObject, IEnumerable<object>>> expected = data =>
                (IEnumerable<object>)Rewritten.Value.SelectList(
                    data["data"],
                    x => 
                        Rewritten.List.Select(
                            Rewritten.List.Select(
                                x["repositoryOwner"]["repositories"]["edges"],
                                y => y["node"]),
                            z => new
                            {
                                Name = z["name"].ToObject<string>(),
                                IsPrivate = z["isPrivate"].ToObject<bool>(),
                                Viewer = Rewritten.Value.Single(
                                    Rewritten.Value.Select(
                                        x["viewer"],
                                        a => new
                                        {
                                            Login = a["login"].ToObject<string>()
                                        }))
                            })).ToList();

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Issues_Select_Nodes()
        {
            var query = new Query()
                .Repository("github", "VisualStudio")
                .Issues(first: 30, labels: new[] { "bug" }, states: new[] { IssueState.Closed })
                .Select(x => x.Nodes)
                .Select(x => new
                {
                    x.Number,
                    x.Title,
                });

            Expression<Func<JObject, IEnumerable<object>>> expected = data =>
                (IEnumerable<object>)Rewritten.List.Select(
                    Rewritten.Value.SelectList(
                        data["data"]["repository"]["issues"],
                        x => x["nodes"]),
                    x => new
                    {
                        Number = x["number"].ToObject<int>(),
                        Title = x["title"].ToObject<string>(),
                    }).ToList();

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }

        [Fact]
        public void Search_User_Name_Via_Edges()
        {
            var query = new Query()
                .Search("foo", SearchType.User, 30)
                .Edges.Select(x => x.Node)
                .Select(x => x.Switch<string>(when =>
                    when.User(user => user.Name)));

            Expression<Func<JObject, IEnumerable<string>>> expected = data =>
                (IEnumerable<string>)Rewritten.List.Select(
                    Rewritten.List.Select(data["data"]["search"]["edges"], x => x["node"]),
                    x => Rewritten.Value.Switch(x, new Dictionary<string, Func<JToken, string>>
                    {
                        { "User", user => user["name"].ToObject<string>() },
                    })).ToList();

            ExpressionRewriterAssertions.AssertExpressionQueryEqual(expected, query);
        }
    }
}
