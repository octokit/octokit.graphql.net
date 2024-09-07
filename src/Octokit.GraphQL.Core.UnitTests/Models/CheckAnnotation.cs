using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class CheckAnnotation : QueryableValue<CheckAnnotation>
    {
        public CheckAnnotation(Expression expression) : base(expression)
        {
        }

        public string Path { get; }

        internal static CheckAnnotation Create(Expression expression)
        {
            return new CheckAnnotation(expression);
        }
    }
}