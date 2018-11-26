using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class CheckAnnotationConnection : QueryableValue<CheckAnnotationConnection>, IPagingConnection<CheckAnnotation>
    {
        public CheckAnnotationConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<CheckAnnotationEdge> Edges => this.CreateProperty(x => x.Edges);

        public IQueryableList<CheckAnnotation> Nodes => this.CreateProperty(x => x.Nodes);

        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Models.PageInfo.Create);

        public int TotalCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static CheckAnnotationConnection Create(Expression expression)
        {
            return new CheckAnnotationConnection(expression);
        }
    }
}