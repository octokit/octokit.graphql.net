using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class CheckSuiteConnection : QueryableValue<CheckSuiteConnection>, IPagingConnection<CheckSuite>
    {
        public CheckSuiteConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<CheckSuiteEdge> Edges => this.CreateProperty(x => x.Edges);

        public IQueryableList<CheckSuite> Nodes => this.CreateProperty(x => x.Nodes);

        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Models.PageInfo.Create);

        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static CheckSuiteConnection Create(Expression expression)
        {
            return new CheckSuiteConnection(expression);
        }
    }
}