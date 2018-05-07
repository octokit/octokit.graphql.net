using System;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.Builders
{
    class AllPagesExpression : Expression
    {
        public AllPagesExpression(MethodCallExpression instance) => Instance = instance;
        public MethodCallExpression Instance { get; }
        public override ExpressionType NodeType => ExpressionType.Extension;
        public override Type Type => Instance.Type;
    }
}
