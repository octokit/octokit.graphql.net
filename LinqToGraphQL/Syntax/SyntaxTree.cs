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

        public FieldSelection AddField(MemberInfo member, MemberInfo alias = null)
        {
            return AddField(head, member, alias);
        }

        public FieldSelection AddField(ISelectionSet parent, MemberInfo member, MemberInfo alias = null)
        {
            return AddField(parent, new FieldSelection(member, alias));
        }

        public Argument AddArgument(string name, object value)
        {
            var result = new Argument(name, value);
            ((FieldSelection)head).Arguments.Add(result);
            return result;
        }

        public InlineFragment AddInlineFragment(Type typeCondition, bool selectTypeName)
        {
            var result = new InlineFragment(typeCondition);

            if (selectTypeName)
            {
                result.Selections.Add(new FieldSelection("__typename", null));
            }

            head.Selections.Add(result);
            head = result;
            return result;
        }

        public IDisposable Bookmark()
        {
            var oldHead = head;
            return Disposable.Create(() => head = oldHead);
        }

        private FieldSelection AddField(ISelectionSet parent, FieldSelection field)
        {
            var existing = field.Alias == null ?
                parent.Selections
                    .OfType<FieldSelection>()
                    .FirstOrDefault(x => x.Name == field.Name && x.Alias == null) :
                null;

            if (existing == null)
            {
                parent.Selections.Add(field);
            }
            else
            {
                field = existing;
            }

            head = field;
            return field;
        }
    }
}
