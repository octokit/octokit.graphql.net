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
        static readonly MethodInfo ToObject = typeof(JToken).GetMethod("ToObject", new Type[0]);
        static readonly ParameterExpression LambdaParameter = Expression.Parameter(typeof(JObject), "x");

        OperationDefinition root;
        Stack<ISyntaxNode> stack = new Stack<ISyntaxNode>();

        public Query<TResult> Build<TResult>(IQueryable<TResult> query)
        {
            var rewritten = Visit(query.Expression);
            var expression = Expression.Lambda<Func<JObject, TResult>>(
                Expression.Call(rewritten, ToObject.MakeGenericMethod(typeof(TResult))),
                LambdaParameter);
            return new Query<TResult>(root, expression);
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
            else if (IsQueryMethod(node.Method))
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
            var selection = new FieldSelection(root, node.Method);

            stack.Push(this.root);
            stack.Push(selection);

            return CreateIndexerExpression(CreateRootExpression(), selection.Name);
        }

        private Expression VisitQueryMethod(MethodCallExpression node)
        {
            var constant = node.Object as ConstantExpression;
            var rootQuery = constant?.Value as IRootQuery;

            if (rootQuery != null)
            {
                var result = VisitRootQuery(node);
                VisitQueryMethodArguments(node.Method.GetParameters(), node.Arguments);
                return result;
            }

            throw new NotImplementedException();
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
                var memberExpression = lambda.Body as MemberExpression;

                if (memberExpression != null)
                {
                    var selection = new FieldSelection((ISelectionSet)stack.Peek(), memberExpression.Member);
                    return CreateIndexerExpression(source, selection.Name);
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

        private bool IsQueryMethod(MethodInfo method)
        {
            return typeof(QueryEntity).IsAssignableFrom(method.DeclaringType);
        }

        private IDisposable Push(ISyntaxNode node)
        {
            stack.Push(node);
            return Disposable.Create(() => stack.Pop());
        }

        private Expression CreateRootExpression()
        {
            return Expression.Call(
                LambdaParameter,
                Indexer,
                Expression.Constant("data"));
        }

        private Expression CreateIndexerExpression(Expression instance, string argument)
        {
            return Expression.Call(
                instance,
                Indexer,
                Expression.Constant(argument));
        }
    }
}
