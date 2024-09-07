using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class CheckRunEdge : QueryableValue<CheckRunEdge>
    {
        public CheckRunEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public CheckRun Node => this.CreateProperty(x => x.Node, CheckRun.Create);

        internal static CheckRunEdge Create(Expression expression)
        {
            return new CheckRunEdge(expression);
        }
    }
}