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

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var left = BookmarkAndVisit(node.Left).AddCast(node.Left.Type);
            var right = BookmarkAndVisit(node.Right).AddCast(node.Right.Type);
            return node.Update(left, node.Conversion, right);
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

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var parameters = Visit(node.Parameters);
            var body = Visit(node.Body);
            return Expression.Lambda(body, parameters);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (IsQueryEntityMember(node.Member))
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
            else
            {
                var source = node.Expression as ParameterExpression;
                ParameterExpression target;

                if (source != null && lambdaParameters.TryGetValue(source, out target))
                {
                    var field = syntax.AddField(node.Member);
                    return target.AddIndexer(field.Name);
                }

                return node;
            }
        }

        protected override MemberAssignment VisitMemberAssignment(MemberAssignment node)
        {
            if (IsQueryEntityMember(node.Expression))
            {
                var expression = Visit(node.Expression).AddCast(node.Expression.Type);
                return Expression.Bind(node.Member, expression);
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
            else if (IsQueryEntityMember(node.Method))
            {
                return VisitQueryMethod(node);
            }

            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            var newArguments = new List<Expression>();
            var index = 0;

            foreach (var arg in node.Arguments)
            {
                var memberExpression = arg as MemberExpression;
                var member = memberExpression?.Member ?? node.Members[index];
                var checkpoint = syntax.Bookmark();

                using (checkpoint)
                {
                    newArguments.Add(Visit(arg).AddCast(arg.Type));
                }

                checkpoint.GetAddedField()?.SetAlias(member);
                ++index;
            }

            return node.Members != null ?
                Expression.New(node.Constructor, newArguments, node.Members) :
                Expression.New(node.Constructor, newArguments);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (IsQueryEntity(node.Type))
            {
                ParameterExpression result;

                if (lambdaParameters.TryGetValue(node, out result))
                {
                    return result;
                }
                else
                {
                    result = Expression.Parameter(typeof(JToken), node.Name);
                    lambdaParameters.Add(node, result);
                }

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
            var parameters = Visit(selectExpression.Parameters);
            var rewrittenSelect = (LambdaExpression)Visit(selectExpression);

            return Expression.Call(
                ExpressionMethods.SelectEntityMethod.MakeGenericMethod(rewrittenSelect.ReturnType),
                instance,
                rewrittenSelect);
        }

        private Expression VisitSelectNew(Expression source, LambdaExpression selectExpression)
        {
            var instance = Visit(source);
            var parameters = Visit(selectExpression.Parameters);
            var newExpression = (NewExpression)Visit(selectExpression.Body);

            var sourceQueryableResultType = GetQueryableResultType(instance.Type);

            if (sourceQueryableResultType == null)
            {
                return Expression.Call(
                    ExpressionMethods.SelectEntityMethod.MakeGenericMethod(newExpression.Constructor.DeclaringType),
                    instance,
                    Expression.Lambda(newExpression, parameters));
            }
            else
            {
                return Expression.Call(
                    ExpressionMethods.SelectMethod.MakeGenericMethod(newExpression.Constructor.DeclaringType),
                    instance,
                    Expression.Lambda(newExpression, parameters));
            }
        }

        private IEnumerable<ParameterExpression> Visit(ReadOnlyCollection<ParameterExpression> parameters)
        {
            foreach (var p in parameters)
            {
                yield return (ParameterExpression)VisitParameter(p);
            }
        }

        private Expression BookmarkAndVisit(Expression left)
        {
            using (syntax.Bookmark())
            {
                return Visit(left);
            }
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

        private static bool IsQueryEntity(Type type)
        {
            return typeof(QueryEntity).IsAssignableFrom(type);
        }

        private static bool IsQueryEntityMember(Expression expression)
        {
            var memberExpression = expression as MemberExpression;
            return memberExpression != null && IsQueryEntityMember(memberExpression.Member);
        }

        private static bool IsQueryEntityMember(MemberInfo member)
        {
            return IsQueryEntity(member.DeclaringType);
        }
    }
}
