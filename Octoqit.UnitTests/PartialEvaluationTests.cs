using System;
using System.Linq;
using LinqToGraphQL.Builders;
using LinqToGraphQL.Deserializers;
using Xunit;

namespace Octoqit.UnitTests
{
    public class PartialEvaluationTests
    {
        [Fact]
        public void RepositoryOwner_Repository_Append_From_Different_Entities()
        {
            throw new NotImplementedException();
////            var expression = new Query()
////                .RepositoryOwner("foo")
////                .Repository("bar")
////                .Select(x => new
////                {
////                    Owner = x.Owner.Select(o => new
////                    {
////                        Thing = x.Name + ": " + o.Login,
////                    }),
////                });

////            string data = @"{
////  ""data"": {
////    ""repositoryOwner"": {
////      ""repository"": {
////        ""name"": ""LinqToGraphQL"",
////        ""owner"": {
////          ""login"": ""grokys""
////        },
////      }
////    }
////  }
////}";

////            var query = new QueryBuilder().Build(expression);
////            dynamic result = new ResponseDeserializer().Deserialize(query, data).Single();

////            Assert.Equal("LinqToGraphQL: grokys", Enumerable.Single(result.Owner).Thing);
        }
    }
}
