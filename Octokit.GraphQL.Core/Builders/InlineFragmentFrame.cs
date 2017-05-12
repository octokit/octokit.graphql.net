using System;
using System.Collections.Generic;
using GraphQLParser.AST;

namespace LinqToGraphQL.Builders
{
    internal class InlineFragmentFrame : Frame, IFieldFrame
    {
        public InlineFragmentFrame(Frame parent, Type type)
            : base(new GraphQLInlineFragment(type))
        {
            ((IFieldFrame)parent).AddSelection(Node);
        }

        public void AddSelection(ASTNode node)
        {
            var field = (GraphQLInlineFragment)Node;
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
