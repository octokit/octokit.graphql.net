using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using GraphQLParser.AST;

namespace LinqToGraphQL.Builders
{
    public class QueryBuilder : ExpressionVisitor
    {
        RootFrame root;
        Stack<Frame> stack = new Stack<Frame>();

        public GraphQLOperationDefinition Build(Expression expression)
        {
            Visit(expression);
            return (GraphQLOperationDefinition)root.Node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (stack.Count == 0)
            {
                var root = node.Value as IRootQuery;
                var queryEntity = node.Value as QueryEntity;

                if (root != null)
                {
                    this.root = new RootFrame(node.Type);
                    stack.Push(this.root);
                }
                else if (queryEntity != null)
                {
                    Visit(queryEntity.Expression);
                }
            }
            else
            {
                var argument = stack.Peek() as ArgumentFrame;

                if (argument != null)
                {
                    argument.SetValue(node.Value);
                }
                else
                {
                    throw new Exception("Unexpected constant.");
                }
            }

            return node;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var memberExpression = node.Body as MemberExpression;
            var newExpression = node.Body as NewExpression;

            if (memberExpression != null)
            {
                stack.Push(new FieldFrame(stack.Peek(), memberExpression.Member));
            }
            else if (newExpression != null)
            {
                foreach (var arg in newExpression.Arguments)
                {
                    using (Checkpoint())
                    {
                        Visit(arg);
                    }
                }
            }
            else
            {
                throw new Exception("Unexpected lambda.");
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Visit(node.Expression);
            stack.Push(new FieldFrame(stack.Peek(), node.Member));
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (IsSelect(node.Method))
            {
                Visit(node.Arguments[0]);
                Visit(node.Arguments[1]);
            }
            else if (IsOfType(node.Method))
            {
                Visit(node.Arguments[0]);
                stack.Push(new InlineFragmentFrame(stack.Peek(), node.Method.GetGenericArguments()[0]));
            }
            else if (IsQueryEntity(node.Method))
            {
                Visit(node.Object);

                stack.Push(new FieldFrame(stack.Peek(), node.Method));

                var parameters = node.Method.GetParameters();

                for (var i = 0; i < node.Arguments.Count; ++i)
                {
                    var arg = node.Arguments[i];

                    if (!IsNullConstant(arg))
                    {
                        using (Push(new ArgumentFrame(stack.Peek().Node, parameters[i].Name)))
                        {
                            Visit(arg);
                        }
                    }
                }
            }
            else
            {
                throw new Exception("Unrecognised method.");
            }

            return node;
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

        private bool IsQueryEntity(MethodInfo method)
        {
            return typeof(QueryEntity).IsAssignableFrom(method.DeclaringType);
        }

        private IDisposable Push(Frame frame)
        {
            stack.Push(frame);
            return Disposable.Create(() => stack.Pop());
        }
    }
}
