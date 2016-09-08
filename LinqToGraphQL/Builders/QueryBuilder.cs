using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using GraphQLParser.AST;

namespace LinqToGraphQL.Builders
{
    public class QueryBuilder : ExpressionVisitor
    {
        GraphQLOperationDefinition root;
        ASTNode current;

        public GraphQLOperationDefinition Build(IQueryable<string> query)
        {
            Visit(query.Expression);
            return root;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            Visit(node.Body);
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            var field = new GraphQLFieldSelection(node.Member.Name.ToCamelCase());
            PushField(field);
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            base.VisitMethodCall(node);

            if (node.Method.DeclaringType == typeof(Queryable))
            {
                if (node.Method.Name == nameof(Queryable.Select))
                {
                    PushSelectionSet();
                    Visit(node.Arguments[1]);
                }
            }
            else
            {
                if (current == null)
                {
                    if (typeof(IRootQuery).IsAssignableFrom(node.Method.DeclaringType))
                    {
                        var operationDefinition = new GraphQLOperationDefinition
                        {
                            Name = new GraphQLName(node.Method.DeclaringType.Name),
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

                var field = new GraphQLFieldSelection(node.Method.Name.ToCamelCase());
                var parameters = node.Method.GetParameters();

                if (node.Arguments.Count > 0)
                {
                    var args = new List<GraphQLArgument>();

                    for (var i = 0; i < node.Arguments.Count; ++i)
                    {
                        GraphQLArgument arg = null;
                        var constant = node.Arguments[i] as ConstantExpression;

                        if (constant != null)
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

                PushField(field);
            }

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            Visit(node.Operand);
            return node;
        }

        private void PushField(GraphQLFieldSelection field)
        {
            var selection = current as GraphQLSelectionSet;

            if (selection == null)
            {
                PushSelectionSet();
                selection = current as GraphQLSelectionSet;
            }

            ((IList<ASTNode>)selection.Selections).Add(field);

            if (field.Arguments != null)
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
