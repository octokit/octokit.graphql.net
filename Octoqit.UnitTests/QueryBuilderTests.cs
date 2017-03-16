using System;
using System.Linq;
using LinqToGraphQL;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Serializers;
using Xunit;

namespace Octoqit.UnitTests
{
    public class QueryBuilderTests
    {
        [Fact]
        public void RepositoryOwner_Repository_Query()
        {
            var expected = @"{
  repositoryOwner(login: ""foo"") {
    repository(name: ""bar"") {
      id
      name
      owner {
        login
      }
      isFork
      isPrivate
    }
  }
}";

            var expression = new Query()
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

            var serializer = new QuerySerializer(2);
            var result = serializer.Serialize(new QueryBuilder().Build(expression).OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RepositoryOwner_Repositories_Query()
        {
            var expected = @"{
  repositoryOwner(login: ""foo"") {
    repositories(first: 30) {
      edges {
        node {
          id
          name
          owner {
            login
          }
          isFork
          isPrivate
        }
      }
    }
  }
}";

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

            var serializer = new QuerySerializer(2);
            var result = serializer.Serialize(new QueryBuilder().Build(expression).OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void RepositoryOwner_Repositories_Query_Viewer()
        {
            var expected = @"{
  repositoryOwner(login: ""foo"") {
    repositories(first: 30) {
      edges {
        node {
          id
          name
          owner {
            login
            avatarURL
          }
          isFork
          isPrivate
        }
      }
    }
  }
  viewer {
    login
    email
  }
}";

            var expression = new Query()
                .Select(root => root
                    .RepositoryOwner("foo")
                    .Repositories(30, null, null, null, null, null, null, null, null)
                    .Edges.Select(x => x.Node)
                    .Select((Repository x) => new
                    {
                        x.Id,
                        x.Name,
                        Owner = x.Owner.Select(o => new
                        {
                            o.Login,
                            AvatarURL = o.AvatarURL(null),
                        }),
                        x.IsFork,
                        x.IsPrivate,
                        Login = root.Viewer.Select(l => l.Login),
                        root.Viewer.Email
                    }));

            var serializer = new QuerySerializer(2);
            var result = serializer.Serialize(new QueryBuilder().Build(expression).OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void User_Email_Query()
        {
            var expected = @"{
  search(query: ""foo"", type: USER, first: 1) {
    userCount
    edges {
      node {
        ... on User {
          __typename
          id
          login
          avatarURL
          websiteURL
          name
        }
      }
    }
  }
}";

            var expression = new Query()
                .Search(first: 1, type: SearchType.User, query: "foo")
                .Select(x => new
                {
                    x.UserCount,
                    User = x.Edges.Select(e => e.Node).OfType<User>().Select((User u) => new
                    {
                        u.Id,
                        u.Login,
                        AvatarURL = u.AvatarURL(null),
                        u.WebsiteURL,
                        u.Name,
                    })
                });

            var serializer = new QuerySerializer(2);
            var result = serializer.Serialize(new QueryBuilder().Build(expression).OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Viewer_Login_Email()
        {
            var expected = @"{
  viewer {
    login
    email
  }
}";

            var expression = new Query().Viewer.Select(x => new { x.Login, x.Email });

            var serializer = new QuerySerializer(2);
            var result = serializer.Serialize(new QueryBuilder().Build(expression).OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Repository_Details_With_Viewer()
        {
            var expected = @"{
  repositoryOwner(login: ""foo"") {
    repositories(first: 30) {
      edges {
        node {
          name
          isPrivate
        }
      }
    }
  }
  viewer
}";

            var expression = new Query()
                .Select(x => x.RepositoryOwner("foo")
                              .Repositories(30, null, null, null, null, null, null, null, null)
                              .Edges
                              .Select(y => y.Node)
                              .Select(y => new
                              {
                                  y.Name,
                                  y.IsPrivate,
                                  x.Viewer
                              }));

            var serializer = new QuerySerializer(2);
            var result = serializer.Serialize(new QueryBuilder().Build(expression).OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Search_User_Name()
        {
            var expected = @"{
  search(query: ""foo"", type: USER, first: 30) {
    nodes {
      ... on User {
        __typename
        name
      }
    }
  }
}";

            var expression = new Query()
                .Search("foo", SearchType.User, 30)
                .Nodes
                .Select(x => x.User.Name);

            var serializer = new QuerySerializer(2);
            var result = serializer.Serialize(new QueryBuilder().Build(expression).OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Search_User_Name_Login()
        {
            var expected = @"{
  search(query: ""foo"", type: USER, first: 30) {
    nodes {
      ... on User {
        __typename
        name
        login
      }
    }
  }
}";

            var expression = new Query()
                .Search("foo", SearchType.User, 30)
                .Nodes
                .Select(x => x.User)
                .Select(x => new
                {
                    x.Name,
                    x.Login,
                });

            var serializer = new QuerySerializer(2);
            var result = serializer.Serialize(new QueryBuilder().Build(expression).OperationDefinition);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Search_User_Name_Via_Edges()
        {
            var expected = @"{
  search(query: ""foo"", type: USER, first: 30) {
    edges {
      node {
        ... on User {
          __typename
          name
        }
      }
    }
  }
}";

            var expression = new Query()
                .Search("foo", SearchType.User, 30)
                .Edges.Select(x => x.Node)
                .Select(x => x.User.Name);

            var serializer = new QuerySerializer(2);
            var result = serializer.Serialize(new QueryBuilder().Build(expression).OperationDefinition);

            Assert.Equal(expected, result);
        }
    }
}
