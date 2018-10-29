using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class Commit : QueryableValue<Commit>
    {
        public Commit(Expression expression) : base(expression)
        {
        }
        public string AbbreviatedOid { get; }

        internal static Commit Create(Expression expression)
        {
            return new Commit(expression);
        }
    }
}