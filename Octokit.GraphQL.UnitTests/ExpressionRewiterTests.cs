using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.Utilities;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Model;
using Xunit;

namespace Octokit.GraphQL.UnitTests
{
    public class ExpressionRewiterTests
    {
        [Fact]
        public void RepositoryOwner_Repository_Query()
        {
            var expression = new Query()
                .RepositoryOwner("foo")
                .Repository("bar")
                .Select((Repository x) => new
                {
                    x.Id,
                    x.Name,
                    Owner = x.Owner.Select(o => new
                    {
                        o.Login
                    }).Single(),
                    x.IsFork,
                    x.IsPrivate,
                });

            Expression<Func<JObject, IEnumerable<object>>> expected = data =>
                Rewritten.List.Select(
                    data["data"]["repositoryOwner"]["repository"],
                    x => new
                    {
                        Id = x["id"].ToObject<string>(),
                        Name = x["name"].ToObject<string>(),
                        Owner = Rewritten.Value.Single(
                            Rewritten.Value.Select(
                                x["owner"],
                                o => new { Login = o["login"].ToObject<string>() })),
                        IsFork = x["isFork"].ToObject<string>(),
                        IsPrivate = x["isPrivate"].ToObject<string>(),
                    });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void RepositoryOwner_Repositories_Query()
        {
            var expression = new Query()
                .RepositoryOwner(login: "foo")
                .Repositories(first: 30)
                .Edges
                .Select(x => x.Node)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Owner = x.Owner.Select(o => new
                    {
                        o.Login
                    }).Single(),
                    x.IsFork,
                    x.IsPrivate,
                });

            Expression<Func<JObject, IEnumerable<object>>> expected = data =>
                Rewritten.List.Select(
                    Rewritten.List.Select(
                        data["data"]["repositoryOwner"]["repositories"]["edges"],
                        x => x["node"]),
                        x => new
                        {
                            Id = x["id"].ToObject<string>(),
                            Name = x["name"].ToObject<string>(),
                            Owner = Rewritten.Value.Single(
                                Rewritten.Value.Select(
                                    x["owner"],
                                    o => new { Login = o["login"].ToObject<string>() })),
                            IsFork = x["isFork"].ToObject<string>(),
                            IsPrivate = x["isPrivate"].ToObject<string>(),
                        });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void Repository_Details_With_Viewer()
        {
            var expression = new Query()
                .Select(x => x.RepositoryOwner("foo")
                              .Repositories(30, null, null, null, null, null, null, null, null)
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
                Rewritten.Value.SelectList(
                    data["data"],
                    x => 
                        Rewritten.List.Select(
                            Rewritten.List.Select(
                                x["repositoryOwner"]["repositories"]["edges"],
                                y => y["node"]),
                            z => new
                            {
                                Name = z["name"].ToObject<string>(),
                                IsPrivate = z["isPrivate"].ToObject<string>(),
                                Viewer = Rewritten.Value.Single(
                                    Rewritten.Value.Select(
                                        x["viewer"],
                                        a => new
                                        {
                                            Login = a["login"].ToObject<string>()
                                        }))
                            }));

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact(Skip = "Not yet working")]
        public void Search_User_Name_Via_Edges()
        {
            var expression = new Query()
                .Search("foo", SearchType.User, 30)
                .Edges.Select(x => x.Node)
                .Select(x => x.User.Name);

            Expression<Func<JObject, IEnumerable<object>>> expected = data =>
                ExpressionMethods.Select(
                    ExpressionMethods.ChildrenOfType(data["data"]["search"]["edges"], "User"),
                    x => x["name"].ToObject<string>());

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }
    }
}
