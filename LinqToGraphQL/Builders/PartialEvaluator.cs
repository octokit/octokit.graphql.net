using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL.Syntax;
using LinqToGraphQL.Utilities;

namespace LinqToGraphQL.Builders
{
    internal class PartialEvaluator : ExpressionVisitor
    {
        private readonly SyntaxTree syntax;
        private readonly IDictionary<ParameterExpression, ParameterExpression> parameters;

        private PartialEvaluator(
            SyntaxTree syntax,
            IDictionary<ParameterExpression, ParameterExpression> parameters)
        {
            this.syntax = syntax;
            this.parameters = parameters;
        }

        public static LambdaExpression Evaluate(
            SyntaxTree syntax,
            LambdaExpression expression,
            IDictionary<ParameterExpression, ParameterExpression> parameters)
        {
            return (LambdaExpression)new PartialEvaluator(syntax, parameters).Visit(expression);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = Visit(node.Left).AddCast(node.Left.Type);
            var right = Visit(node.Right).AddCast(node.Right.Type);
            return node.Update(left, node.Conversion, right);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var parameters = RewriteParamters(node.Parameters);
            var body = Visit(node.Body);
            return Expression.Lambda(body, parameters);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var source = node.Expression as ParameterExpression;
            ParameterExpression target;

            if (source != null && parameters.TryGetValue(source, out target))
            {
                var field = syntax.AddField(node.Member);
                return target.AddIndexer(field.Name);
            }

            return node;
        }

        private ParameterExpression RewriteParameter(ParameterExpression parameter)
        {
            ParameterExpression result;

            if (parameters.TryGetValue(parameter, out result))
            {
                return result;
            }

            return parameter;
        }

        private IEnumerable<ParameterExpression> RewriteParamters(IEnumerable<ParameterExpression> source)
        {
            return source.Select(x => RewriteParameter(x));
        }
    }
}
