using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.Builders
{
    class SubqueryExpression : Expression
    {
        public SubqueryExpression(SubQuery subQuery, MethodCallExpression methodCall)
        {
            SubQuery = subQuery;
            MethodCall = methodCall;
        }

        public MethodCallExpression MethodCall { get; }
        public override ExpressionType NodeType => ExpressionType.Extension;
        public SubQuery SubQuery { get; }
        public override Type Type => MethodCall.Type;
    }
}
