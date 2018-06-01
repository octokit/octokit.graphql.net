using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class AddCommentPayload : QueryableValue<AddCommentPayload>
    {
        public AddCommentPayload(Expression expression) : base(expression)
        {
        }

        public string ClientMutationId { get; }
        public IssueCommentEdge CommentEdge => this.CreateProperty(x => x.CommentEdge, IssueCommentEdge.Create);
        public INode Subject => this.CreateProperty(x => x.Subject, StubINode.Create);

        internal static AddCommentPayload Create(Expression expression)
        {
            return new AddCommentPayload(expression);
        }
    }
}
