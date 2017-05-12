using System;
using System.Collections.Generic;
using GraphQLParser.AST;

namespace LinqToGraphQL.Builders
{
    internal class RootFrame : Frame, IFieldFrame
    {
        public RootFrame(Type type)
            : base(CreateNode(type))
        {
        }

        public void AddSelection(ASTNode node)
        {
            var field = (GraphQLOperationDefinition)Node;
            var selectionSet = field.SelectionSet ?? (field.SelectionSet = new GraphQLSelectionSet());
            var selections = (List<ASTNode>)selectionSet.Selections;

            if (selections == null)
            {
                selections = new List<ASTNode>();
            }

            selections.Add(node);
        }

        private static ASTNode CreateNode(Type type)
        {
            if (typeof(IRootQuery).IsAssignableFrom(type))
            {
                return new GraphQLOperationDefinition
                {
                    Name = new GraphQLName(type.Name),
                    SelectionSet = new GraphQLSelectionSet
                    {
                        Selections = new List<ASTNode>(),
                    },
                    Operation = OperationType.Query,
                };
            }
            else
            {
                throw new Exception($"The type '{type}' is not a query root.");
            }
        }
    }
}
