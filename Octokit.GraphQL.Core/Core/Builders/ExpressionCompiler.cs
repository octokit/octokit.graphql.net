using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.Builders
{
    public static class ExpressionCompiler
    {
        static Dictionary<object, Expression> sourceExpression;

        public static bool IsUnitTesting { get; set; }

        public static T Compile<T>(Expression<T> expression)
        {
            var compiled = expression.Compile();

            if (IsUnitTesting)
            {
                if (sourceExpression == null)
                {
                    sourceExpression = new Dictionary<object, Expression>();
                }

                sourceExpression.Add(compiled, expression);
            }

            return compiled;
        }

        public static Expression GetSourceExpression(object func)
        {
            return sourceExpression[func];
        }
    }
}
