using System;
using Octokit.GraphQL.Model;
using Xunit;

namespace Octokit.GraphQL.UnitTests
{
    public class QueryBuilderTests
    {
        [Fact]
        public void RepositoryOwner_Repository_Query()
        {
            var expected = @"query {
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
                    }).Single(),
                    x.IsFork,
                    x.IsPrivate,
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void RepositoryOwner_Repositories_Query()
        {
            var expected = @"query {
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
                    }).Single(),
                    x.IsFork,
                    x.IsPrivate,
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void RepositoryOwner_Repositories_Query_Viewer()
        {
            var expected = @"query {
  repositoryOwner(login: ""foo"") {
    repositories(first: 30) {
      edges {
        node {
          id
          name
          owner {
            login
            avatarUrl
          }
          isFork
          isPrivate
        }
      }
    }
  }
  login: viewer {
    login
  }
  email: viewer {
    email
  }
}";

            var expression = new Query()
                .Select(root => root
                    .RepositoryOwner("foo")
                    .Repositories(30, null, null, null, null, null, null, null, null, null)
                    .Edges.Select(x => x.Node)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        Owner = x.Owner.Select(o => new
                        {
                            o.Login,
                            AvatarUrl = o.AvatarUrl(null),
                        }).Single(),
                        x.IsFork,
                        x.IsPrivate,
                        Login = root.Viewer.Select(l => l.Login).Single(),
                        root.Viewer.Email
                    }));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void User_Email_Query()
        {
            var expected = @"query {
  search(query: ""foo"", type: USER, first: 1) {
    userCount
    edges {
      node {
        __typename
        ... on User {
          id
          login
          avatarUrl
          websiteUrl
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
                    User = x.Edges.Select(e => e.Node).OfType<User>().Select(u => new
                    {
                        u.Id,
                        u.Login,
                        AvatarUrl = u.AvatarUrl(null),
                        u.WebsiteUrl,
                        u.Name,
                    }).ToList(),
                });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Viewer_Login_Email()
        {
            var expected = @"query {
  viewer {
    login
    email
  }
}";

            var expression = new Query().Viewer.Select(x => new { x.Login, x.Email });

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Repository_Details_With_Viewer()
        {
            var expected = @"query {
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
  viewer {
    login
  }
}";

            var expression = new Query()
                .Select(x => x.RepositoryOwner("foo")
                              .Repositories(30, null, null, null, null, null, null, null, null, null)
                              .Edges
                              .Select(y => y.Node)
                              .Select(y => new
                              {
                                  y.Name,
                                  y.IsPrivate,
                                  Viewer = x.Viewer.Select(z => new
                                  {
                                      z.Login
                                  }).Single()
                              }));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Search_User_Name()
        {
            var expected = @"query {
  search(query: ""foo"", type: USER, first: 30) {
    nodes {
      __typename
      ... on User {
        name
      }
    }
  }
}";

            var expression = new Query()
                .Search("foo", SearchType.User, 30)
                .Nodes
                .Select(x => x.Switch<string>(when =>
                    when.User(user => user.Name)));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact]
        public void Search_User_Name_Login()
        {
            var expected = @"query {
  search(query: ""foo"", type: USER, first: 30) {
    nodes {
      __typename
      ... on User {
        name
        login
      }
    }
  }
}";

            var expression = new Query()
                .Search("foo", SearchType.User, 30)
                .Nodes
                .Select(x => x.Switch<object>(when =>
                    when.User(user => new
                    {
                        user.Name,
                        user.Login,
                    })));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }

        [Fact(Skip = "Not yet working")]
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
                .Select(x => x.Switch<string>(when =>
                    when.User(user => user.Name)));

            var query = expression.Compile();

            Assert.Equal(expected, query.ToString(), ignoreLineEndingDifferences: true);
        }
    }
}
