using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Octokit.GraphQL.Core.Builders
{
    class AliasedExpression : Expression
    {
        public AliasedExpression(Expression inner, MemberInfo alias)
        {
            Inner = inner;
            Alias = alias;
        }

        public Expression Inner { get; }
        public MemberInfo Alias { get; }

        public static Expression WrapIfNeeded(Expression inner, MemberInfo alias)
        {
            return alias != null ? new AliasedExpression(inner, alias) : inner;
        }
    }
}
