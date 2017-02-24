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
        Stack<ISyntaxNode> stack;
        Dictionary<ParameterExpression, ParameterExpression> lambdaParameters;

        public Query<TResult> Build<TResult>(IQueryable<TResult> query)
        {
            root = null;
            stack = new Stack<ISyntaxNode>();
            lambdaParameters = new Dictionary<ParameterExpression, ParameterExpression>();

            var rewritten = Visit(query.Expression);
            var expression = Expression.Lambda<Func<JObject, IEnumerable<TResult>>>(
                Cast(rewritten, typeof(IQueryable<TResult>)),
                RootDataParameter);
            return new Query<TResult>(root, expression);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (stack.Count == 0)
            {
                var rootQuery = node.Value as IRootQuery;
                var queryEntity = node.Value as QueryEntity;

                if (rootQuery != null)
                {
                    root = new OperationDefinition(OperationType.Query, node.Type.Name);
                    stack.Push(this.root);
                    return CreateIndexerExpression(RootDataParameter, "data");
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
                    var selection = new FieldSelection((ISelectionSet)stack.Peek(), node.Member);
                    stack.Push(selection);
                    return CreateIndexerExpression(
                        instance,
                        selection.Name);
                }
                else if (parameterExpression != null)
                {
                    var selection = new FieldSelection((ISelectionSet)stack.Peek(), node.Member);
                    stack.Push(selection);
                    return CreateIndexerExpression(
                        Visit(parameterExpression),
                        selection.Name);
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
                stack.Push(new InlineFragment((ISelectionSet)stack.Peek(), node.Method.GetGenericArguments()[0]));
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
            var selection = new FieldSelection((ISelectionSet)stack.Peek(), node.Method);

            stack.Push(selection);
            VisitQueryMethodArguments(node.Method.GetParameters(), node.Arguments);

            return CreateIndexerExpression(instance, selection.Name);
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
                    var argument = new Argument((FieldSelection)stack.Peek(), parameter.Name);
                    argument.Value = constantArgument.Value;
                }
            }
        }

        private Expression VisitSelect(Expression source, Expression selectExpression)
        {
            var lambda = selectExpression.GetLambda();

            switch (lambda.Body.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return VisitSelectMember(source, lambda);
                case ExpressionType.New:
                    return VisitSelectNew(source, lambda);
                default:
                    throw new NotImplementedException();
            }
        }

        private Expression VisitSelectMember(Expression source, LambdaExpression selectExpression)
        {
            var instance = Visit(source);
            var memberExpression = (MemberExpression)selectExpression.Body;
            var selection = new FieldSelection((ISelectionSet)stack.Peek(), memberExpression.Member);
            var lambdaParameter = RewriteLambdaParameter(selectExpression.Parameters[0]);

            stack.Push(selection);

            return Expression.Call(
                ExpressionMethods.SelectEntityMethod.MakeGenericMethod(typeof(JToken)),
                instance,
                Expression.Lambda(
                    CreateIndexerExpression(lambdaParameter, selection.Name),
                    lambdaParameter));
        }

        private Expression VisitSelectNew(Expression source, LambdaExpression selectExpression)
        {
            var instance = Visit(source);
            var lambdaParameter = RewriteLambdaParameter(selectExpression.Parameters[0]);
            var newExpression = (NewExpression)selectExpression.Body;
            var newArguments = new List<Expression>();
            var index = 0;

            foreach (var arg in newExpression.Arguments)
            {
                var memberName = SelectionSet.GetIdentifier(newExpression.Members[index]);

                using (Checkpoint())
                {
                    newArguments.Add(Cast(Visit(arg), arg.Type));
                }

                var field = (FieldSelection)((ISelectionSet)stack.Peek()).Selections.Last();

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
                        lambdaParameter));
            }
            else
            {
                return Expression.Call(
                    ExpressionMethods.SelectMethod.MakeGenericMethod(newExpression.Constructor.DeclaringType),
                    instance,
                    Expression.Lambda(
                        Expression.New(newExpression.Constructor, newArguments, newExpression.Members),
                        lambdaParameter));
            }
        }

        private IDisposable Checkpoint()
        {
            var count = stack.Count;
            return Disposable.Create(() =>
            {
                while (stack.Count > count)
                {
                    stack.Pop();
                }

                if (stack.Count == 0) { }
            });
        }

        private ParameterExpression RewriteLambdaParameter(ParameterExpression expression)
        {
            var result = Expression.Parameter(typeof(JToken), expression.Name);
            lambdaParameters.Add(expression, result);
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

        private static Expression Cast(Expression expression, Type type)
        {
            if (expression.Type == type)
            {
                return expression;
            }
            else if (typeof(JToken).IsAssignableFrom(expression.Type))
            {
                return Expression.Call(
                    expression,
                    ExpressionMethods.JTokenToObject.MakeGenericMethod(type));
            }
            else if (GetQueryableResultType(type) != null && 
                     GetQueryableResultType(expression.Type) == typeof(JToken))
            {
                var queryType = type.GetGenericArguments()[0];
                var methodCall = expression as MethodCallExpression;

                if (methodCall != null && IsJsonSelect(methodCall.Method))
                {
                    var instance = methodCall.Arguments[0];
                    var lambda = methodCall.Arguments[1].GetLambda();
                    return Expression.Call(
                        ExpressionMethods.SelectEntityMethod.MakeGenericMethod(queryType),
                        instance,
                        Expression.Lambda(
                            Cast(lambda.Body, queryType),
                            lambda.Parameters));
                }
            }

            throw new NotImplementedException(
                $"Don't know how to cast '{expression}' to '{type}'.");
        }

        private static Expression CreateIndexerExpression(Expression instance, string argument)
        {
            return Expression.Call(
                instance,
                ExpressionMethods.JTokenIndexer,
                Expression.Constant(argument));
        }
    }
}
