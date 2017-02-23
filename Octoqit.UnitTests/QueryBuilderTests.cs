using System.Linq;
using LinqToGraphQL;
using LinqToGraphQL.Builders;
using Xunit;

namespace Octoqit.UnitTests
{
//    public class QueryBuilderTests
//    {
//        [Fact]
//        public void RepositoryOwner_Repository_Query()
//        {
//            var expected = @"query RootQuery {
//  repositoryOwner(login: ""foo"") {
//    repository(name: ""bar"") {
//      id
//      name
//      owner {
//        login
//      }
//      isFork
//      isPrivate
//    }
//  }
//}";

//            var query = new RootQuery()
//                .RepositoryOwner("foo")
//                .Repository("bar")
//                .Select(x => new
//                {
//                    x.Id,
//                    x.Name,
//                    Owner = x.Owner.Select(o => new
//                    {
//                        o.Login
//                    }),
//                    x.IsFork,
//                    x.IsPrivate,
//                });

//            var serializer = new QuerySerializer(2);
//            var result = serializer.Serialize(new QueryBuilder().Build(query.Expression));

//            Assert.Equal(expected, result);
//        }

//        [Fact]
//        public void RepositoryOwner_Repositories_Query()
//        {
//            var expected = @"query RootQuery {
//  repositoryOwner(login: ""foo"") {
//    repositories(first: 30) {
//      edges {
//        node {
//          id
//          name
//          owner {
//            login
//          }
//          isFork
//          isPrivate
//        }
//      }
//    }
//  }
//}";

//            var query = new RootQuery()
//                .RepositoryOwner(login: "foo")
//                .Repositories(first: 30)
//                .Edges
//                .Select(x => x.Node)
//                .Select(x => new
//                {
//                    x.Id,
//                    x.Name,
//                    Owner = x.Owner.Select(o => new
//                    {
//                        o.Login
//                    }),
//                    x.IsFork,
//                    x.IsPrivate,
//                });

//            var serializer = new QuerySerializer(2);
//            var result = serializer.Serialize(new QueryBuilder().Build(query.Expression));

//            Assert.Equal(expected, result);
//        }

//        [Fact]
//        public void User_Email_Query()
//        {
//            var expected = @"query RootQuery {
//  search(type: USER, query: ""foo"", first: 1) {
//    userCount
//    edges {
//      node {
//        ... on User {
//          id
//          login
//          avatarURL
//          websiteURL
//          name
//        }
//      }
//    }
//  }
//}";

//            var query = new RootQuery()
//                .Search(first: 1, type: SearchType.User, query: "foo")
//                .Select(x => new
//                {
//                    x.UserCount,
//                    User = x.Edges.Select(e => e.Node).OfType<User>().Select(u => new
//                    {
//                        u.Id,
//                        u.Login,
//                        u.AvatarUrl,
//                        u.WebsiteUrl,
//                        u.Name,
//                    })
//                });

//            var serializer = new QuerySerializer(2);
//            var result = serializer.Serialize(new QueryBuilder().Build(query.Expression));

//            Assert.Equal(expected, result);
//        }

//        [Fact]
//        public void Viewer_Login_Email()
//        {
//            var expected = @"query RootQuery {
//  viewer {
//    login
//    email
//  }
//}";

//            var query = new RootQuery().Viewer.Select(x => new { x.Login, x.Email });

//            var serializer = new QuerySerializer(2);
//            var result = serializer.Serialize(new QueryBuilder().Build(query.Expression));

//            Assert.Equal(expected, result);
//        }
//    }
}

