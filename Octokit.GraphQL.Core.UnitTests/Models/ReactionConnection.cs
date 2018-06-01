using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class ReactionConnection : QueryableValue<ReactionConnection>, IPagingConnection<Reaction>
    {
        public ReactionConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<Reaction> Nodes => this.CreateProperty(x => x.Nodes);
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, PageInfo.Create);
        public int TotalCount { get; }
        public bool ViewerHasReacted { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static ReactionConnection Create(Expression expression)
        {
            return new ReactionConnection(expression);
        }
    }
}