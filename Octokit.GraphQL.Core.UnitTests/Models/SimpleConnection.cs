using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class SimpleConnection : QueryableValue<SimpleConnection>, IPagingConnection<Simple>
    {
        public SimpleConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<Simple> Nodes => this.CreateProperty(x => x.Nodes);
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Models.PageInfo.Create);
        public int TotalCount { get; }
        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static SimpleConnection Create(Expression expression)
        {
            return new SimpleConnection(expression);
        }
    }
}
