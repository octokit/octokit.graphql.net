using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class OrganizationConnection : QueryableValue<OrganizationConnection>, IPagingConnection<Organization>
    {
        public OrganizationConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<OrganizationEdge> Edges => this.CreateProperty(x => x.Edges);

        public IQueryableList<Organization> Nodes => this.CreateProperty(x => x.Nodes);

        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, PageInfo.Create);

        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static OrganizationConnection Create(Expression expression)
        {
            return new OrganizationConnection(expression);
        }
    }
}