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
        ASTNode current;

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
            PushField(field, true);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable))
            {
                if (node.Method.Name == nameof(Queryable.Select))
                {
                    Visit(node.Arguments[0]);
                    PushSelectionSet();
                    Visit(node.Arguments[1]);
                }
            }
            else
            {
                base.VisitMethodCall(node);

                if (current == null)
                {
                    PushRoot(node.Method.DeclaringType);
                }
                else
                {
                    if (!typeof(QueryEntity).IsAssignableFrom(node.Method.DeclaringType))
                    {
                        throw new Exception("Invalid method " + node.Method);
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

                PushField(field, true);
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
                    PushField(field, false);
                }
                else
                {
                    Visit(value);
                }
            }

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            Visit(node.Operand);
            return node;
        }

        private void PushRoot(Type type)
        {
            if (current != null)
            {
                throw new Exception("Expression contains multiple roots.");
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
                current = operationDefinition.SelectionSet;
            }
            else
            {
                throw new Exception("Could not find root query.");
            }
        }

        private void PushField(GraphQLFieldSelection field, bool updateCurrent)
        {
            var selection = current as GraphQLSelectionSet;

            if (selection == null)
            {
                PushSelectionSet();
                selection = current as GraphQLSelectionSet;
            }

            ((IList<ASTNode>)selection.Selections).Add(field);

            if (updateCurrent)
            {
                current = field;
            }
        }

        private void PushSelectionSet()
        {
            var selectionSet = current as GraphQLSelectionSet;

            if (selectionSet == null)
            {
                var operationDefinition = current as GraphQLOperationDefinition;
                var field = current as GraphQLFieldSelection;

                if (operationDefinition != null)
                {
                    operationDefinition.SelectionSet = new GraphQLSelectionSet
                    {
                        Selections = new List<ASTNode>(),
                    };

                    current = operationDefinition.SelectionSet;
                }
                else if (field != null)
                {
                    field.SelectionSet = new GraphQLSelectionSet
                    {
                        Selections = new List<ASTNode>(),
                    };

                    current = field.SelectionSet;
                }
                else
                {
                    throw new Exception("Cannot push a selection set at this point.");
                }
            }
            else
            {
                selectionSet.Selections = new List<ASTNode>();
            }
        }
    }
}
