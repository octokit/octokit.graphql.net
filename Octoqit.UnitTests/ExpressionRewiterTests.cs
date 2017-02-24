using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Utilities;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Octoqit.UnitTests
{
    public class ExpressionRewiterTests
    {
        [Fact]
        public void RepositoryOwner_Repository_Query()
        {
            var expression = new RootQuery()
                .RepositoryOwner("foo")
                .Repository("bar")
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Owner = x.Owner.Select(o => new
                    {
                        o.Login
                    }),
                    x.IsFork,
                    x.IsPrivate,
                });

            Expression<Func<JObject, IEnumerable<object>>> expected = data =>
                JsonUtilities.Select(
                    data["data"]["repositoryOwner"]["repository"],
                    x => new
                    {
                        Id = x["id"].ToObject<string>(),
                        Name = x["name"].ToObject<string>(),
                        Owner = JsonUtilities.Select(x["owner"], o => new { Login = o["login"].ToObject<string>() }),
                        IsFork = x["isFork"].ToObject<string>(),
                        IsPrivate = x["isPrivate"].ToObject<string>(),
                    });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }

        [Fact]
        public void RepositoryOwner_Repositories_Query()
        {
            var expression = new RootQuery()
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
                    }),
                    x.IsFork,
                    x.IsPrivate,
                });

            Expression<Func<JObject, IEnumerable<object>>> expected = data =>
                JsonUtilities.Select(
                    data["data"]["repositoryOwner"]["repositories"],
                    x => new
                    {
                        Id = x["id"].ToObject<string>(),
                        Name = x["name"].ToObject<string>(),
                        Owner = JsonUtilities.Select(x["owner"], o => new { Login = o["login"].ToObject<string>() }),
                        IsFork = x["isFork"].ToObject<string>(),
                        IsPrivate = x["isPrivate"].ToObject<string>(),
                    });

            var query = new QueryBuilder().Build(expression);
            Assert.Equal(expected.ToString(), query.Expression.ToString());
        }
    }
}
