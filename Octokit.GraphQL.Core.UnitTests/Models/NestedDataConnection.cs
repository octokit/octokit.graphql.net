using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class NestedDataConnection : QueryableValue<NestedDataConnection>, IPagingConnection<NestedData>
    {
        public NestedDataConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<NestedData> Nodes => this.CreateProperty(x => x.Nodes);
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Models.PageInfo.Create);
        public int TotalCount { get; }
        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static NestedDataConnection Create(Expression expression)
        {
            return new NestedDataConnection(expression);
        }
    }
}
