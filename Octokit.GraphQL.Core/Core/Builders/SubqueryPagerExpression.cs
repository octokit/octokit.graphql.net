using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.Builders
{
    class SubqueryPagerExpression : Expression
    {
        public SubqueryPagerExpression(MethodCallExpression methodCall)
        {
            MethodCall = methodCall;
        }

        public MethodCallExpression MethodCall { get; }
        public override ExpressionType NodeType => ExpressionType.Extension;
        public override Type Type => MethodCall.Type;
    }
}
