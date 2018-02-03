using System;
using System.Linq.Expressions;
using System.Reflection;
using Octokit.GraphQL.Core;

namespace Octokit.GraphQL.UnitTests
{
    /// <summary>
    /// Normalizes a query expression for comparison:
    /// </summary>
    /// <remarks>
    /// Normalizes a query expression for comparison such that:
    /// 
    /// - `new Query()` becomes `value(Query)`.
    /// - `Convert(Convert(Arg<T>))` becomes `value(Arg<T>)`
    /// - Captured `Arg<T>`s become `value(Arg<T>)`
    /// </remarks>
    class ExpectedExpression : ExpressionVisitor
    {
        public static Expression Normalize<T>(Expression<Func<T>> f)
        {
            return Normalize(f.Body);
        }

        public static Expression Normalize(Expression e)
        {
            var ee = new ExpectedExpression();
            return ee.Visit(e);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member is FieldInfo f)
            {
                if (IsNullableArg(f.FieldType))
                {
                    var arg = Expression.Lambda(node).Compile().DynamicInvoke();
                    return Expression.Constant(arg, node.Type);
                }
            }

            return base.VisitMember(node);
        }

        protected override Expression VisitNew(NewExpression node)
        {
            if (node.Type == typeof(Query))
            {
                return Expression.Constant(new Query());
            }

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (IsNullableArg(node.Type))
            {
                if (node.Operand is NewExpression ne)
                {
                    var arg = Expression.Lambda(ne).Compile().DynamicInvoke();
                    return Expression.Constant(arg, node.Type);
                }
            }

            return base.VisitUnary(node);
        }

        private bool IsNullableArg(Type type)
        {
            var t = type.GetTypeInfo();

            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var nt = t.GetGenericArguments()[0];

                return nt.IsGenericType && nt.GetGenericTypeDefinition() == typeof(Arg<>);
            }

            return false;
        }
    }
}
