using System;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    [AssociatedQuery(typeof(Query))]
    public class Mutation : QueryableValue<Mutation>, IMutation
    {
        public Mutation() : base(null)
        {
        }

        public AddCommentPayload AddComment(Arg<AddCommentInput> input) => this.CreateMethodCall(x => x.AddComment(input), AddCommentPayload.Create);
    }
}
