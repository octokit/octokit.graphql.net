using System;
using System.Linq;
using System.Reflection;

namespace LinqToGraphQL.Syntax
{
    public class SyntaxTree
    {
        private OperationDefinition root;
        private ISelectionSet head;

        public OperationDefinition Root => root;
        public ISelectionSet Head => head;

        public OperationDefinition AddRoot(OperationType type, string name)
        {
            root = new OperationDefinition(type, name);
            head = root;
            return root;
        }

        public FieldSelection AddField(ISelectionSet parent, MemberInfo member)
        {
            var result = new FieldSelection(member);
            var existing = parent.Selections
                .OfType<FieldSelection>()
                .FirstOrDefault(x => x.Name == result.Name && x.Alias == null);

            if (existing == null)
            {
                parent.Selections.Add(result);
            }
            else
            {
                result = existing;
            }

            if (parent == head)
            {
                head = result;
            }

            return result;
        }

        public Argument AddArgument(string name, object value)
        {
            var result = new Argument(name, value);
            ((FieldSelection)head).Arguments.Add(result);
            return result;
        }

        public InlineFragment AddInlineFragment(Type typeCondition)
        {
            var result = new InlineFragment(typeCondition);
            head.Selections.Add(result);
            head = result;
            return result;
        }

        public BookmarkState Bookmark() => new BookmarkState(this);

        public class BookmarkState : IDisposable
        {
            private SyntaxTree tree;
            private ISelectionSet reset;
            private int selectionCount;

            internal BookmarkState(SyntaxTree tree)
            {
                this.tree = tree;
                reset = tree.head;
                selectionCount = reset.Selections.Count;
            }

            public void Dispose()
            {
                tree.head = reset;
            }

            public FieldSelection GetAddedField()
            {
                if (tree.head != reset)
                {
                    throw new Exception("You must reset to the checkpoint before calling GetAddedField.");
                }
                else if (tree.head.Selections.Count == selectionCount + 1)
                {
                    return tree.head.Selections[selectionCount] as FieldSelection;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
