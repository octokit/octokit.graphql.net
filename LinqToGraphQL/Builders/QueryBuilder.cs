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
        static readonly MethodInfo Indexer = typeof(JToken).GetMethod("get_Item");
        static readonly MethodInfo JsonSelect = typeof(JsonUtilities).GetMethod(nameof(JsonUtilities.Select));
        static readonly MethodInfo ToObject = typeof(JToken).GetMethod(nameof(JToken.ToObject), new Type[0]);
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
                rewritten,
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
                        selection.Name,
                        selection.ResultType);
                }
                else if (parameterExpression != null)
                {
                    var selection = new FieldSelection((ISelectionSet)stack.Peek(), node.Member);
                    stack.Push(selection);
                    return CreateIndexerExpression(
                        Visit(parameterExpression),
                        selection.Name,
                        selection.ResultType);
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
            source = Visit(source);

            var lambda = selectExpression.GetLambda();

            if (lambda != null)
            {
                var lambdaParameter = RewriteLambdaParameter(lambda.Parameters[0]);

                switch (lambda.Body.NodeType)
                {
                    case ExpressionType.MemberAccess:
                        var memberExpression = lambda.Body as MemberExpression;
                        var selection = new FieldSelection((ISelectionSet)stack.Peek(), memberExpression.Member);

                        return Expression.Call(
                            JsonSelect.MakeGenericMethod(memberExpression.Type),
                            source,
                            Expression.Lambda(
                                CreateIndexerExpression(lambdaParameter, selection.Name, selection.ResultType),
                                lambdaParameter));

                    case ExpressionType.New:
                        var newExpression = lambda.Body as NewExpression;
                        var newArguments = new List<Expression>();
                        var index = 0;

                        foreach (var arg in newExpression.Arguments)
                        {
                            var memberName = newExpression.Members[index].Name.ToCamelCase();

                            using (Checkpoint())
                            {
                                newArguments.Add(Visit(arg));

                                var field = (FieldSelection)stack.Peek();

                                if (field.Name != memberName)
                                {
                                    field.Alias = memberName;
                                }
                            }

                            ++index;
                        }

                        return Expression.Call(
                            JsonSelect.MakeGenericMethod(newExpression.Constructor.DeclaringType),
                            source,
                            Expression.Lambda(
                                Expression.New(newExpression.Constructor, newArguments, newExpression.Members),
                                lambdaParameter));
                }
            }
            else
            {
                selectExpression = Visit(selectExpression);
            }

            var indexer = typeof(JToken).GetMethod("get_Item");
            return Expression.Call(source, indexer, selectExpression);
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

        private bool IsOfType(MethodInfo method)
        {
            return method.DeclaringType == typeof(Queryable) &&
                method.Name == nameof(Queryable.OfType) &&
                method.GetParameters().Length == 1 &&
                method.GetGenericArguments().Length == 1;
        }

        private bool IsQueryable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IQueryable<>);
        }

        private bool IsSelect(MethodInfo method)
        {
            return (method.DeclaringType == typeof(Queryable) &&
                method.Name == nameof(Queryable.Select) &&
                method.GetParameters().Length == 2) ||
                (method.DeclaringType == typeof(QueryEntityExtensions) &&
                method.Name == nameof(QueryEntityExtensions.Select) &&
                method.GetParameters().Length == 2);
        }

        private bool IsQueryMember(MemberInfo member)
        {
            return typeof(QueryEntity).IsAssignableFrom(member.DeclaringType);
        }

        private Expression CreateIndexerExpression(Expression instance, string argument)
        {
            return Expression.Call(
                instance,
                Indexer,
                Expression.Constant(argument));
        }

        private Expression CreateIndexerExpression(Expression instance, string argument, Type cast)
        {
            if (!IsQueryable(cast))
            {
                return Expression.Call(
                    CreateIndexerExpression(instance, argument),
                    ToObject.MakeGenericMethod(cast));
            }
            else
            {
                return CreateIndexerExpression(instance, argument);
            }
        }

        private ParameterExpression RewriteLambdaParameter(ParameterExpression expression)
        {
            var result = Expression.Parameter(typeof(JToken), expression.Name);
            lambdaParameters.Add(expression, result);
            return result;
        }
    }
}
