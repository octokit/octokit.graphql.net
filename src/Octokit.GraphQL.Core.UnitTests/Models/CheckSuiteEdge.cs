using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class CheckSuiteEdge : QueryableValue<CheckSuiteEdge>
    {
        public CheckSuiteEdge(Expression expression) : base(expression)
        {
        }

        public string Cursor { get; }

        public CheckSuite Node => this.CreateProperty(x => x.Node, CheckSuite.Create);

        internal static CheckSuiteEdge Create(Expression expression)
        {
            return new CheckSuiteEdge(expression);
        }
    }
}