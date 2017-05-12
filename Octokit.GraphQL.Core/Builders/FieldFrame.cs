using System;
using System.Collections.Generic;
using System.Reflection;
using GraphQLParser.AST;

namespace LinqToGraphQL.Builders
{
    internal class FieldFrame : Frame, IFieldFrame
    {
        public FieldFrame(Frame parent, MemberInfo member)
            : base(new GraphQLFieldSelection(GetIdentifier(member)))
        {
            ((IFieldFrame)parent).AddSelection(Node);
        }

        public void AddSelection(ASTNode node)
        {
            var field = (GraphQLFieldSelection)Node;
            var selectionSet = field.SelectionSet ?? (field.SelectionSet = new GraphQLSelectionSet());
            var selections = (List<ASTNode>)selectionSet.Selections;

            if (selections == null)
            {
                selectionSet.Selections = selections = new List<ASTNode>();
            }

            selections.Add(node);
        }
    }
}
