using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class Issue : QueryableValue<Issue>
    {
        public Issue(Expression expression) : base(expression)
        {
        }

        public string Body { get; }
        public bool Closed { get; }
        public IssueCommentConnection Comments(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Comments(first, after, last, before), Models.IssueCommentConnection.Create);
        public Milestone Milestone => this.CreateProperty(x => x.Milestone, Models.Milestone.Create);
        public int Number { get; }
        public string Title { get; }

        internal static Issue Create(Expression expression)
        {
            return new Issue(expression);
        }
    }
}