using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqToGraphQL;
using LinqToGraphQL.Syntax;
using LinqToGraphQL.Utilities;
using Newtonsoft.Json.Linq;

namespace LinqToGraphQL.Builders
{
    public class QueryBuilder : ExpressionVisitor
    {
        static readonly MethodInfo Indexer = typeof(JToken).GetMethod("get_Item");
        static readonly MethodInfo JsonSelect = typeof(JsonExtensions).GetMethod(nameof(JsonExtensions.JsonSelect));
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
            var expression = Expression.Lambda<Func<JObject, TResult>>(
                rewritten,
                RootDataParameter);
            return new Query<TResult>(root, expression);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (IsQueryMember(node.Member))
            {
                var parameterExpression = node.Expression as ParameterExpression;

                if (parameterExpression != null)
                {
                    var selection = new FieldSelection((ISelectionSet)stack.Peek(), node.Member);
                    stack.Push(selection);
                    return CreateIndexerExpression(
                        lambdaParameters[parameterExpression],
                        selection.Name,
                        selection.ResultType);
                }
            }

            throw new NotImplementedException();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (IsSelect(node.Method))
            {
                return VisitSelect(node.Arguments[0], node.Arguments[1]);
            }
            //else if (IsOfType(node.Method))
            //{
            //    Visit(node.Arguments[0]);
            //    stack.Push(new InlineFragment((ISelectionSet)stack.Peek(), node.Method.GetGenericArguments()[0]));
            //}
            else if (IsQueryMember(node.Method))
            {
                return VisitQueryMethod(node);
            }
            else
            {
                throw new Exception("Unrecognised method.");
            }
        }

        private Expression VisitRootQuery(MethodCallExpression node)
        {
            root = new OperationDefinition(OperationType.Query, node.Object.Type.Name);
            stack.Push(this.root);
            return CreateIndexerExpression(RootDataParameter, "data");
        }

        private Expression VisitQueryMethod(MethodCallExpression node)
        {
            var constant = node.Object as ConstantExpression;
            var instance = default(Expression);

            if (constant != null)
            {
                var rootQuery = constant.Value as IRootQuery;
                var queryEntity = constant.Value as QueryEntity;

                if (rootQuery != null)
                {
                    instance = VisitRootQuery(node);
                }
                else if (queryEntity != null)
                {
                    instance = Visit(queryEntity.Expression);
                }
            }

            if (instance == null)
            {
                instance = Visit(node.Object);
            }

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
                var arg = arguments[i] as ConstantExpression;
                
                if (arg == null)
                {
                    throw new NotSupportedException("Only constant expressions supported as field arguments.");
                }

                if (arg.Value != null)
                {
                    var argument = new Argument((FieldSelection)stack.Peek(), parameter.Name);
                    argument.Value = arg.Value;
                }
            }
        }

        private Expression VisitSelect(Expression source, Expression selectExpression)
        {
            source = Visit(source);

            var lambda = selectExpression.GetLambda();

            if (lambda != null)
            {
                switch (lambda.Body.NodeType)
                {
                    case ExpressionType.MemberAccess:
                        var memberExpression = lambda.Body as MemberExpression;
                        var selection = new FieldSelection((ISelectionSet)stack.Peek(), memberExpression.Member);
                        return CreateIndexerExpression(source, selection.Name, selection.ResultType);

                    case ExpressionType.New:
                        var newExpression = lambda.Body as NewExpression;
                        var newArguments = new List<Expression>();
                        var lambdaParameter = RewriteLambdaParameter(lambda.Parameters[0]);

                        foreach (var arg in newExpression.Arguments)
                        {
                            using (Checkpoint())
                            {
                                newArguments.Add(Visit(arg));
                            }
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

        private bool IsNullConstant(Expression arg)
        {
            var constant = arg as ConstantExpression;
            return constant != null && constant.Value == null;
        }

        private bool IsOfType(MethodInfo method)
        {
            return method.DeclaringType == typeof(Queryable) &&
                method.Name == nameof(Queryable.OfType) &&
                method.GetParameters().Length == 1 &&
                method.GetGenericArguments().Length == 1;
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

        private IDisposable Push(ISyntaxNode node)
        {
            stack.Push(node);
            return Disposable.Create(() => stack.Pop());
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
            return Expression.Call(
                CreateIndexerExpression(instance, argument),
                ToObject.MakeGenericMethod(cast));
        }

        private ParameterExpression RewriteLambdaParameter(ParameterExpression expression)
        {
            var result = Expression.Parameter(typeof(JToken), expression.Name);
            lambdaParameters.Add(expression, result);
            return result;
        }
    }
}
