using System;
using System.Linq;
using LinqToGraphQL;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Deserializers;
using Xunit;

namespace Octoqit.UnitTests
{
    public class ResponseDeserializerTests
    {
        [Fact]
        public void RepositoryOwner_Repository_Query()
        {
            var expression = new RootQuery()
                .RepositoryOwner("grokys")
                .Repository("VisualStudio")
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

            string data = @"{
  ""data"": {
    ""repositoryOwner"": {
      ""repository"": {
        ""id"": ""1234"",
        ""name"": ""LinqToGraphQL"",
        ""owner"": {
          ""login"": ""grokys""
        },
        ""isFork"": false,
        ""isPrivate"": false
      }
    }
  }
}";

            var query = new QueryBuilder().Build(expression);
            dynamic result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.Equal("1234", result.Id);
            Assert.Equal("LinqToGraphQL", result.Name);
            Assert.Equal("grokys", Enumerable.Single(result.Owner).Login);
            Assert.Equal(false, result.IsFork);
            Assert.Equal(false, result.IsPrivate);
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

            string data = @"{
  ""data"": {
    ""repositoryOwner"": {
      ""repositories"": {
        ""edges"": [
          {
            ""node"": {
              ""id"": ""1234"",
              ""name"": ""LinqToGraphQL"",
              ""owner"": {
                ""login"": ""grokys""
              },
              ""isFork"": false,
              ""isPrivate"": false
            }
          },
          {
            ""node"": {
              ""id"": ""2345"",
              ""name"": ""Avalonia"",
              ""owner"": {
                ""login"": ""grokys""
              },
              ""isFork"": true,
              ""isPrivate"": false
            }
          }
        ]
      }
    }
  }
}";
            var query = new QueryBuilder().Build(expression);
            dynamic result = new ResponseDeserializer().Deserialize(query, data);

            Assert.Equal(
                new[]
                {
                    new
                    {
                        Id = "1234",
                        Name = "LinqToGraphQL",
                        Owner = new
                        {
                            Login = "grokys",
                        },
                        IsFork = false,
                        IsPrivate = false,
                    },
                    new
                    {
                        Id = "2345",
                        Name = "Avalonia",
                        Owner = new
                        {
                            Login = "grokys",
                        },
                        IsFork = true,
                        IsPrivate = false,
                    },
                },
                result);
        }

        [Fact]
        public void Viewer_Email()
        {
            var expression = new RootQuery().Viewer.Select(x => new { x.Email });

            string data = @"{
  ""data"": {
    ""viewer"": {
      ""email"": ""grokys@gmail.com""
    }
  }
}";
            var query = new QueryBuilder().Build(expression);
            dynamic result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.Equal("grokys@gmail.com", result.Email);
        }

        [Fact]
        public void Viewer_Login_Email()
        {
            var expression = new RootQuery().Viewer.Select(x => new { x.Login, x.Email });
            string data = @"{
  ""data"": {
    ""viewer"": {
      ""login"": ""grokys"",
      ""email"": ""grokys@gmail.com""
    }
  }
}";
            var query = new QueryBuilder().Build(expression);
            dynamic result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.Equal("grokys", result.Login);
            Assert.Equal("grokys@gmail.com", result.Email);
        }
    }
}
