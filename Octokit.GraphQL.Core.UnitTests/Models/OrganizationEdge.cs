using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class OrganizationEdge : QueryableValue<OrganizationEdge>
    {
        public OrganizationEdge(Expression expression) : base(expression)
        {
        }

        public string Cursor { get; }

        public Organization Node => this.CreateProperty(x => x.Node, Organization.Create);

        internal static OrganizationEdge Create(Expression expression)
        {
            return new OrganizationEdge(expression);
        }
    }
}