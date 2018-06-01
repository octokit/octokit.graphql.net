using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class IssueComment : QueryableValue<IssueComment>
    {
        public IssueComment(Expression expression) : base(expression)
        {
        }

        public ID Id { get; set; }
        public string Body { get; }
        public DateTimeOffset? PublishedAt { get; }
        public ReactionConnection Reactions(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Reactions(first, after, last, before), ReactionConnection.Create);

        internal static IssueComment Create(Expression expression)
        {
            return new IssueComment(expression);
        }
    }
}