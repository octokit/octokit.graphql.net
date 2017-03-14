using System;
using System.Collections.Generic;
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
            var expression = new Query()
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
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.Equal("1234", result.Id);
            Assert.Equal("LinqToGraphQL", result.Name);
            Assert.Equal("grokys", Enumerable.Single(result.Owner).Login);
            Assert.Equal(false, result.IsFork);
            Assert.Equal(false, result.IsPrivate);
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
            var result = new ResponseDeserializer().Deserialize(query, data).ToList();

            var item = result.ElementAt(0);
            Assert.Equal("1234", item.Id);
            Assert.Equal("LinqToGraphQL", item.Name);
            Assert.Equal("grokys", Queryable.Single(item.Owner).Login);
            Assert.False(item.IsFork);
            Assert.False(item.IsPrivate);

            item = result.ElementAt(1);
            Assert.Equal("2345", item.Id);
            Assert.Equal("Avalonia", item.Name);
            Assert.Equal("grokys", Queryable.Single(item.Owner).Login);
            Assert.True(item.IsFork);
            Assert.False(item.IsPrivate);
        }

        [Fact]
        public void Viewer_Email()
        {
            var expression = new Query().Viewer.Select(x => new { x.Email });

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
            var expression = new Query().Viewer.Select(x => new { x.Login, x.Email });
            string data = @"{
  ""data"": {
    ""viewer"": {
      ""login"": ""grokys"",
      ""email"": ""grokys@gmail.com""
    }
  }
}";
            var query = new QueryBuilder().Build(expression);
            var result = new ResponseDeserializer().Deserialize(query, data).Single();

            Assert.Equal("grokys", result.Login);
            Assert.Equal("grokys@gmail.com", result.Email);
        }

        [Fact]
        public void Should_Throw_Exception()
        {
            var data = @"{
  ""data"":null,
  ""errors"":[
    {
      ""message"":""Error message."",
      ""locations"":[
        {
          ""line"":5,
          ""column"":6
        }
      ]
    }
  ]
}";
            var expression = new Query().Viewer.Select(x => new { x.Login, x.Email });
            var query = new QueryBuilder().Build(expression);
            var thrown = true;

            try
            {
                new ResponseDeserializer().Deserialize(query, data);
            }
            catch (GraphQLQueryException e)
            {
                thrown = e.Message == "Error message." &&
                         e.Line == 5 &&
                         e.Column == 6;
            }

            Assert.True(thrown);
        }
    }
}
