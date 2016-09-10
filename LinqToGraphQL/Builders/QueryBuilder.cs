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
        GraphQLOperationDefinition root;
        Stack<GraphQLSelectionSet> stack = new Stack<GraphQLSelectionSet>();

        public GraphQLOperationDefinition Build(Expression expression)
        {
            Visit(expression);
            return root;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            var root = node.Value as IRootQuery;
            var queryEntity = node.Value as QueryEntity;

            if (root != null)
            {
                PushRoot(node.Value.GetType());
            }
            else if (queryEntity != null)
            {
                Visit(queryEntity.Expression);
            }

            return node;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            Visit(node.Body);
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            Visit(node.Expression);
            var field = new GraphQLFieldSelection(node.Member.Name.ToCamelCase());
            Push(field);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable))
            {
                if (node.Method.Name == nameof(Queryable.Select))
                {
                    base.VisitMethodCall(node);
                }
                else if (node.Method.Name == nameof(Queryable.OfType))
                {
                    base.VisitMethodCall(node);
                    Push(new GraphQLInlineFragment(node.Method.GetGenericArguments()[0]));                    
                }
                else
                {
                    throw new NotSupportedException(
                        $"The method '{node.Method.DeclaringType.Name}.{node.Method.Name}' " + 
                        "is not currently supported in GraphQL queries.");
                }
            }
            else if (node.Method.DeclaringType == typeof(QueryEntityExtensions))
            {
                if (node.Method.Name == nameof(QueryEntityExtensions.Select))
                {
                    base.VisitMethodCall(node);
                }
                else
                {
                    throw new NotSupportedException(
                        $"The method '{node.Method.DeclaringType.Name}.{node.Method.Name}' " +
                        "is not currently supported in GraphQL queries.");
                }
            }
            else
            {
                base.VisitMethodCall(node);

                if (root == null)
                {
                    PushRoot(node.Method.DeclaringType);
                }
                else
                {
                    if (!typeof(QueryEntity).IsAssignableFrom(node.Method.DeclaringType))
                    {
                        throw new NotSupportedException(
                            $"The method '{node.Method.DeclaringType.Name}.{node.Method.Name}' " +
                            "cannot be compiled to GraphQL.");
                    }
                }

                var field = new GraphQLFieldSelection(node.Method.Name.ToCamelCase());
                var parameters = node.Method.GetParameters();

                if (node.Arguments.Count > 0)
                {
                    var args = new List<GraphQLArgument>();

                    for (var i = 0; i < node.Arguments.Count; ++i)
                    {
                        GraphQLArgument arg = null;
                        var constant = node.Arguments[i] as ConstantExpression;

                        if (constant != null && constant.Value != null)
                        {
                            arg = new GraphQLArgument(parameters[i].Name, constant.Value);
                        }

                        if (arg != null)
                        {
                            args.Add(arg);
                        }
                    }

                    field.Arguments = args;
                }

                Push(field);
            }

            return node;
        }

        protected override Expression VisitNew(NewExpression node)
        {
            for (var i = 0; i < node.Members.Count; ++i)
            {
                var member = node.Members[i];
                var value = node.Arguments[i];
                var memberValue = value as MemberExpression;

                if (memberValue != null && memberValue.Member is PropertyInfo)
                {
                    var field = new GraphQLFieldSelection(member.Name.ToCamelCase());
                    Add(field);
                }
                else
                {
                    Visit(value);
                }
            }

            Pop();

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            Visit(node.Operand);
            return node;
        }

        private void Add(GraphQLFieldSelection field)
        {
            var current = stack.Peek();
            ((IList<ASTNode>)current.Selections).Add(field);
        }

        private void Push(GraphQLFieldSelection field)
        {
            Add(field);

            field.SelectionSet = new GraphQLSelectionSet
            {
                Selections = new List<ASTNode>(),
            };

            stack.Push(field.SelectionSet);
        }

        private void Push(GraphQLInlineFragment fragment)
        {
            var current = stack.Peek();
            ((IList<ASTNode>)current.Selections).Add(fragment);

            fragment.SelectionSet = new GraphQLSelectionSet
            {
                Selections = new List<ASTNode>(),
            };

            stack.Push(fragment.SelectionSet);
        }

        private void PushRoot(Type type)
        {
            if (root != null)
            {
                throw new InvalidOperationException("Expression contains multiple roots.");
            }

            if (typeof(IRootQuery).IsAssignableFrom(type))
            {
                var operationDefinition = new GraphQLOperationDefinition
                {
                    Name = new GraphQLName(type.Name),
                    SelectionSet = new GraphQLSelectionSet
                    {
                        Selections = new List<ASTNode>(),
                    },
                    Operation = OperationType.Query,
                };

                root = operationDefinition;
                stack.Push(operationDefinition.SelectionSet);
            }
            else
            {
                throw new InvalidOperationException("Could not find root query.");
            }
        }

        private void Pop()
        {
            stack.Pop();
        }
    }
}
