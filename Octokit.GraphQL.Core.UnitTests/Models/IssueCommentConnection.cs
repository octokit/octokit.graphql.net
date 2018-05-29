using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class IssueCommentConnection : QueryableValue<IssueCommentConnection>, IPagingConnection<IssueComment>
    {
        public IssueCommentConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<IssueComment> Nodes => this.CreateProperty(x => x.Nodes);
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Models.PageInfo.Create);
        public int TotalCount { get; }
        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static IssueCommentConnection Create(Expression expression)
        {
            return new IssueCommentConnection(expression);
        }
    }
}