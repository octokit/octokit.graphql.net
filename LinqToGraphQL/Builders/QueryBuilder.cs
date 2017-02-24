using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqToGraphQL.Syntax;
using LinqToGraphQL.Utilities;
using Newtonsoft.Json.Linq;

namespace LinqToGraphQL.Builders
{
    public class QueryBuilder : ExpressionVisitor
    {
        static readonly ParameterExpression RootDataParameter = Expression.Parameter(typeof(JObject), "data");

        OperationDefinition root;
        SyntaxTree syntax;
        Dictionary<ParameterExpression, ParameterExpression> lambdaParameters;

        public Query<TResult> Build<TResult>(IQueryable<TResult> query)
        {
            root = null;
            syntax = new SyntaxTree();
            lambdaParameters = new Dictionary<ParameterExpression, ParameterExpression>();

            var rewritten = Visit(query.Expression);
            var expression = Expression.Lambda<Func<JObject, IEnumerable<TResult>>>(
                rewritten.AddCast(typeof(IQueryable<TResult>)),
                RootDataParameter);
            return new Query<TResult>(root, expression);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (syntax.Root == null)
            {
                var rootQuery = node.Value as IRootQuery;
                var queryEntity = node.Value as QueryEntity;

                if (rootQuery != null)
                {
                    root = syntax.AddRoot(OperationType.Query, node.Type.Name);
                    return RootDataParameter.AddIndexer("data");
                }
                else if (queryEntity != null)
                {
                    return Visit(queryEntity.Expression);
                }
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (IsQueryMember(node.Member))
            {
                var constantExpression = node.Expression as ConstantExpression;
                var parameterExpression = node.Expression as ParameterExpression;

                if (constantExpression != null)
                {
                    var instance = Visit(constantExpression);
                    var field = syntax.AddField(node.Member);
                    return instance.AddIndexer(field.Name);
                }
                else if (parameterExpression != null)
                {
                    var field = syntax.AddField(node.Member);
                    return Visit(parameterExpression).AddIndexer(field.Name);
                }

                throw new NotImplementedException();
            }

            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (IsSelect(node.Method))
            {
                return VisitSelect(node.Arguments[0], node.Arguments[1]);
            }
            else if (IsOfType(node.Method))
            {
                var result = Visit(node.Arguments[0]);
                syntax.AddInlineFragment(node.Method.GetGenericArguments()[0]);
                return result;
            }
            else if (IsQueryMember(node.Method))
            {
                return VisitQueryMethod(node);
            }

            return node;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            var result = node;

            if (lambdaParameters.TryGetValue(node, out result))
            {
                return result;
            }

            return node;
        }

        private Expression VisitQueryMethod(MethodCallExpression node)
        {
            var queryEntity = (node.Object as ConstantExpression)?.Value as QueryEntity;
            var instance = Visit(queryEntity?.Expression ?? node.Object);
            var field = syntax.AddField(node.Method);

            VisitQueryMethodArguments(node.Method.GetParameters(), node.Arguments);

            return instance.AddIndexer(field.Name);
        }

        private void VisitQueryMethodArguments(ParameterInfo[] parameters, ReadOnlyCollection<Expression> arguments)
        {
            for (var i = 0; i < parameters.Length; ++i)
            {
                var parameter = parameters[i];
                var constantArgument = arguments[i] as ConstantExpression
                    ?? (arguments[i] as UnaryExpression)?.Operand as ConstantExpression;

                if (constantArgument == null)
                {
                    throw new NotSupportedException("Non-constant field arguments not yet supported.");
                }

                if (constantArgument.Value != null)
                {
                    syntax.AddArgument(parameter.Name, constantArgument.Value);
                }
            }
        }

        private Expression VisitSelect(Expression source, Expression selectExpression)
        {
            var lambda = selectExpression.GetLambda();

            switch (lambda.Body.NodeType)
            {
                case ExpressionType.New:
                    return VisitSelectNew(source, lambda);
                default:
                    return VisitSelectMember(source, lambda);
            }
        }

        private Expression VisitSelectMember(Expression source, LambdaExpression selectExpression)
        {
            var instance = Visit(source);
            var parameters = RewriteLambdaParameters(selectExpression.Parameters);
            var rewrittenSelect = PartialEvaluator.Evaluate(syntax, selectExpression, lambdaParameters);

            return Expression.Call(
                ExpressionMethods.SelectEntityMethod.MakeGenericMethod(rewrittenSelect.ReturnType),
                instance,
                rewrittenSelect);
        }

        private Expression VisitSelectNew(Expression source, LambdaExpression selectExpression)
        {
            var instance = Visit(source);
            var parameters = RewriteLambdaParameters(selectExpression.Parameters);
            var newExpression = (NewExpression)selectExpression.Body;
            var newArguments = new List<Expression>();
            var index = 0;

            foreach (var arg in newExpression.Arguments)
            {
                var memberExpression = arg as MemberExpression;
                var memberName = memberExpression != null ?
                    SelectionSet.GetIdentifier(memberExpression.Member) :
                    SelectionSet.GetIdentifier(newExpression.Members[index]);
                var checkpoint = syntax.Bookmark();

                using (checkpoint)
                {
                    newArguments.Add(Visit(arg).AddCast(arg.Type));
                }

                var field = checkpoint.GetAddedField();

                if (field.Name != memberName)
                {
                    field.Alias = memberName;
                }

                ++index;
            }

            var sourceQueryableResultType = GetQueryableResultType(instance.Type);

            if (sourceQueryableResultType == null)
            {
                return Expression.Call(
                    ExpressionMethods.SelectEntityMethod.MakeGenericMethod(newExpression.Constructor.DeclaringType),
                    instance,
                    Expression.Lambda(
                        Expression.New(newExpression.Constructor, newArguments, newExpression.Members),
                        parameters));
            }
            else
            {
                return Expression.Call(
                    ExpressionMethods.SelectMethod.MakeGenericMethod(newExpression.Constructor.DeclaringType),
                    instance,
                    Expression.Lambda(
                        Expression.New(newExpression.Constructor, newArguments, newExpression.Members),
                        parameters));
            }
        }

        private IEnumerable<ParameterExpression> RewriteLambdaParameters(IEnumerable<ParameterExpression> parameters)
        {
            var result = new List<ParameterExpression>();

            foreach (var expression in parameters)
            {
                var p = Expression.Parameter(typeof(JToken), expression.Name);
                lambdaParameters.Add(expression, p);
                result.Add(p);
            }

            return result;
        }

        private static Type GetQueryableResultType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IQueryable<>) ?
                type.GetGenericArguments()[0] : null;
        }

        private static bool IsOfType(MethodInfo method)
        {
            return method.DeclaringType == typeof(Queryable) &&
                method.Name == nameof(Queryable.OfType) &&
                method.GetParameters().Length == 1 &&
                method.GetGenericArguments().Length == 1;
        }

        private static bool IsJsonSelect(MethodInfo method)
        {
            return (method.DeclaringType == typeof(ExpressionMethods) &&
                method.Name == nameof(ExpressionMethods.SelectEntity) &&
                method.GetParameters().Length == 2);
        }

        private static bool IsSelect(MethodInfo method)
        {
            return (method.DeclaringType == typeof(Queryable) &&
                method.Name == nameof(Queryable.Select) &&
                method.GetParameters().Length == 2) ||
                (method.DeclaringType == typeof(QueryEntityExtensions) &&
                method.Name == nameof(QueryEntityExtensions.Select) &&
                method.GetParameters().Length == 2);
        }

        private static bool IsQueryMember(MemberInfo member)
        {
            return typeof(QueryEntity).IsAssignableFrom(member.DeclaringType);
        }
    }
}
