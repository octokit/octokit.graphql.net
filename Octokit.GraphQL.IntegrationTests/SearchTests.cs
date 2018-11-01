using System.Collections.Generic;
using System.Linq;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Octokit.GraphQL.Model;

namespace Octokit.GraphQL.IntegrationTests
{
    public class SearchTests : IntegrationTestBase
    {
        [IntegrationTest]
        public void Should_Query_Commits()
        {
            var query = new Query()
                .Search("language:JavaScript stars:>10000", SearchType.Issue, first: 3)
                .Nodes
                .Select(item => item.Switch<string>(when =>
                    when.Issue(issue => issue.Id.Value)));

            var queryBuilder = new QueryBuilder();
            var queryString = queryBuilder.Build(query);;

            var vars1 = new Dictionary<string, object>() { { "a", 1 }, { "b", true }, { "c", "foor" } };
            var vars2 = new Dictionary<string, object>() { { "a", 2 }, { "b", false }, { "c", "bar" } };

            //var results = Connection.Run(query).Result.ToArray();
        }
    }
}