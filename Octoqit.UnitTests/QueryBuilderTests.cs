using System.Linq;
using LinqToGraphQL.Builders;
using Xunit;

namespace Octoqit.UnitTests
{
    public class QueryBuilderTests
    {
        [Fact]
        public void RepositoryOwner_Repository_Query()
        {
   //         var expected = "query RootQuery{repositoryOwner(login:\"foo\"){repository(name:\"bar\"){id name owner{login}isFork isPrivate stars(first:1){totalCount}}}}";

            var expected = @"query RootQuery {
  repositoryOwner(login: ""foo"") {
    repository(name: ""bar"") {
      id
      name
      owner {
        login
      }
      isFork
      isPrivate
      stars(first: 1) {
        totalCount
      }
    }
  }
}";

            var query = new RootQuery()
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
                    StarCount = x.Stars.Count(),
                });

            var serializer = new QuerySerializer(2);
            var result = serializer.Serialize(new QueryBuilder().Build(query.Expression));

            Assert.Equal(expected, result);
        }

        //        [Fact]
        //        public void RepositoryOwner_Repositories_Query()
        //        {
        //            var expected = @"
        //query RootQuery {
        // repositoryOwner(login: ""foo"") {
        //    reposities(first: 30) {
        //      edges {
        //        node {
        //          id
        //          name
        //          owner {
        //            login
        //          }
        //          isFork
        //          isPrivate
        //          stars(first: 1) {
        //            totalCount
        //          }
        //        }
        //      }
        //    }
        //  }
        //}";

        //            var connection = new MockConnection();
        //            var query = new RootQuery(connection)
        //                .RepositoryOwner(login: "foo").Single()
        //                .Repositories(first: 30).Edges
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
        //                    StarCount = x.Stars.Count(),
        //                });

        //            var result = query.ToList();

        //            Assert.Equal(expected, connection.Query);
        //        }

        //        [Fact]
        //        public void User_Email_Query()
        //        {
        //            var expected = @"
        //query RootQuery {
        //  search(first: 1, type: USER, query: ""foo"") {
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

        //            var connection = new MockConnection();
        //            var query = new RootQuery(connection)
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

        //            var result = query.Single();

        //            Assert.Equal(expected, connection.Query);
        //        }
    }
}

