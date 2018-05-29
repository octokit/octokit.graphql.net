using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class IssueComment : QueryableValue<IssueComment>
    {
        public IssueComment(Expression expression) : base(expression)
        {
        }

        public string Body { get; }
        public DateTimeOffset? PublishedAt { get; }

        internal static IssueComment Create(Expression expression)
        {
            return new IssueComment(expression);
        }
    }
}