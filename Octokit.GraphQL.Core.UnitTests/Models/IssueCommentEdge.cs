using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class IssueCommentEdge : QueryableValue<IssueCommentEdge>
    {
        public IssueCommentEdge(Expression expression) : base(expression)
        {
        }

        public string Cursor { get; }
        public IssueComment Node => this.CreateProperty(x => x.Node, IssueComment.Create);

        internal static IssueCommentEdge Create(Expression expression)
        {
            return new IssueCommentEdge(expression);
        }
    }
}
