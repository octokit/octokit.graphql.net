using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.Builders
{
    public static class ExpressionCompiler
    {
        static ConcurrentDictionary<object, Expression> sourceExpression;

        public static bool IsUnitTesting { get; set; }

        public static T Compile<T>(Expression<T> expression)
        {
            var compiled = expression.Compile();

            if (IsUnitTesting)
            {
                if (sourceExpression == null)
                {
                    sourceExpression = new ConcurrentDictionary<object, Expression>();
                }

                sourceExpression[compiled] = expression;
            }

            return compiled;
        }

        public static Expression GetSourceExpression(object func)
        {
            return sourceExpression[func];
        }
    }
}
