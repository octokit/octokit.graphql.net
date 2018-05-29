using System;
using System.Linq;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Model;
using Xunit;

namespace Octokit.GraphQL.UnitTests
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
                    }).Single(),
                    x.IsFork,
                    x.IsPrivate,
                });

            string data = @"{
  ""data"": {
    ""repositoryOwner"": {
      ""repository"": {
        ""id"": ""1234"",
        ""name"": ""Octokit.GraphQL.Core"",
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
            var result = query.Deserialize(data);

            Assert.Equal("1234", result.Id.Value);
            Assert.Equal("Octokit.GraphQL.Core", result.Name);
            Assert.Equal("grokys", result.Owner.Login);
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
                    }).Single(),
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
              ""name"": ""Octokit.GraphQL.Core"",
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
            var result = query.Deserialize(data).ToList();

            var item = result.ElementAt(0);
            Assert.Equal("1234", item.Id.Value);
            Assert.Equal("Octokit.GraphQL.Core", item.Name);
            Assert.Equal("grokys", item.Owner.Login);
            Assert.False(item.IsFork);
            Assert.False(item.IsPrivate);

            item = result.ElementAt(1);
            Assert.Equal("2345", item.Id.Value);
            Assert.Equal("Avalonia", item.Name);
            Assert.Equal("grokys", item.Owner.Login);
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
            var result = query.Deserialize(data);

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
            var result = query.Deserialize(data);

            Assert.Equal("grokys", result.Login);
            Assert.Equal("grokys@gmail.com", result.Email);
        }

        [Fact]
        public void AvatarUrl()
        {
            var expression = new Query().Viewer.Select(x => new { AvatarUrl = x.AvatarUrl(null) });
            string data = @"{
  ""data"": {
    ""viewer"": {
      ""avatarUrl"": ""https://foo/bar"",
    }
  }
}";
            var query = new QueryBuilder().Build(expression);
            var result = query.Deserialize(data);

            Assert.Equal("https://foo/bar", result.AvatarUrl);
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
                var result = query.Deserialize(data);
            }
            catch (GraphQLQueryException e)
            {
                thrown = e.Message == "Error message." &&
                         e.Line == 5 &&
                         e.Column == 6;
            }

            Assert.True(thrown);
        }

        [Fact]
        public void PullRequest_Review_State_ChangesRequested()
        {
            var expression = new Query()
                .Repository("grokys", "VisualStudio")
                .PullRequest(1)
                .Reviews(first : 30)
                .Select(x => x.Nodes)
                .Select(x => new
                {
                    x.State
                });

            string data = @"{
  ""data"": {
    ""repository"": {
      ""pullRequest"": {
        ""reviews"": {
          ""nodes"": [{
            ""state"": ""CHANGES_REQUESTED""
          }]
        }
      }
    }
  }
}";

            var query = new QueryBuilder().Build(expression);
            var result = query.Deserialize(data).ToList();

            Assert.Equal(PullRequestReviewState.ChangesRequested, result[0].State);
        }

        [Fact]
        public void PullRequest_Review_State_ChangesRequested_Cast_To_Int()
        {
            var expression = new Query()
                .Repository("grokys", "VisualStudio")
                .PullRequest(1)
                .Reviews(first: 30)
                .Select(x => x.Nodes)
                .Select(x => new
                {
                    State = (int)x.State
                });

            string data = @"{
  ""data"": {
    ""repository"": {
      ""pullRequest"": {
        ""reviews"": {
          ""nodes"": [{
            ""state"": ""CHANGES_REQUESTED""
          }]
        }
      }
    }
  }
}";

            var query = new QueryBuilder().Build(expression);
            var result = query.Deserialize(data).ToList();

            Assert.Equal(3, result[0].State);
        }
    }
}
