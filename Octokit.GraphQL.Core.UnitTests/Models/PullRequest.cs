using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class PullRequest : QueryableValue<PullRequest>
    {
        public PullRequest(Expression expression) : base(expression)
        {
        }

        public string Body { get; }
        public int Number { get; }
        public string Title { get; }
        public PullRequestTimelineConnection Timeline(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Timeline(first, after, last, before), Models.PullRequestTimelineConnection.Create);

        internal static PullRequest Create(Expression expression)
        {
            return new PullRequest(expression);
        }
    }
}