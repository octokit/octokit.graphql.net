using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class PullRequestTimelineConnection : QueryableValue<PullRequestTimelineConnection>, IPagingConnection<PullRequestTimelineItem>
    {
        public PullRequestTimelineConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<PullRequestTimelineItem> Nodes => this.CreateProperty(x => x.Nodes);
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Models.PageInfo.Create);
        public int TotalCount { get; }
        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static PullRequestTimelineConnection Create(Expression expression)
        {
            return new PullRequestTimelineConnection(expression);
        }
    }
}