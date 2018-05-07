using System;
using System.Linq;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.Deserializers;
using Xunit;

namespace Octokit.GraphQL.UnitTests
{
    public class PartialEvaluationTests
    {
        [Fact]
        public void RepositoryOwner_Repository_Append_From_Different_Entities()
        {
            var expression = new Query()
                .RepositoryOwner("foo")
                .Repository("bar")
                .Select(x => new
                {
                    Owner = x.Owner.Select(o => new
                    {
                        Thing = x.Name + ": " + o.Login,
                    }).Single(),
                });

            string data = @"{
  ""data"": {
    ""repositoryOwner"": {
      ""repository"": {
        ""name"": ""Octokit.GraphQL.Core"",
        ""owner"": {
          ""login"": ""grokys""
        },
      }
    }
  }
}";

            var query = new QueryBuilder().Build(expression);
            var result = query.Deserialize(data);

            Assert.Equal("Octokit.GraphQL.Core: grokys", result.Owner.Thing);
        }
    }
}
