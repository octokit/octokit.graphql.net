using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Octokit.GraphQL.Core.Syntax
{
    public class SyntaxTree
    {
        public OperationDefinition Root { get; private set; }

        public ISelectionSet Head { get; private set; }
        public IList<FieldSelection> FieldStack { get; private set; }

        public OperationDefinition AddRoot(OperationType type, string name)
        {
            Root = new OperationDefinition(type, name);
            Head = Root;
            FieldStack = new List<FieldSelection>();
            return Root;
        }

        public FieldSelection AddField(string member, string alias = null)
        {
            return AddField(Head, new FieldSelection(member, alias));
        }

        public FieldSelection AddField(MemberInfo member, MemberInfo alias = null)
        {
            return AddField(Head, member, alias);
        }

        public FieldSelection AddField(ISelectionSet parent, MemberInfo member, MemberInfo alias = null)
        {
            return AddField(parent, new FieldSelection(member, alias));
        }

        public Argument AddArgument(string name, object value)
        {
            var result = new Argument(name, value);
            ((FieldSelection)Head).Arguments.Add(result);
            return result;
        }

        public InlineFragment AddInlineFragment(Type typeCondition, bool selectTypeName)
        {
            var result = new InlineFragment(typeCondition);

            if (selectTypeName)
            {
                Head.Selections.Add(new FieldSelection("__typename", null));
            }

            Head.Selections.Add(result);
            Head = result;
            return result;
        }

        public VariableDefinition AddVariableDefinition(Type type, bool isNullable, string name)
        {
            var result = Root.VariableDefinitions.SingleOrDefault(x => x.Name == name);

            if (result != null && result.Type != VariableDefinition.ToTypeName(type, isNullable))
            {
                throw new InvalidOperationException(
                    $"A variable called '{name}' has already been added with a different type.");
            }

            result = result ?? new VariableDefinition(type, isNullable, name);
            Root.VariableDefinitions.Add(result);
            return result;
        }

        public IDisposable Bookmark()
        {
            var oldHead = Head;
            var oldStack = FieldStack.ToList();

            return Disposable.Create(() =>
            {
                Head = oldHead;
                FieldStack = oldStack;
            });
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

            Head = field;
            FieldStack.Add(field);
            return field;
        }
    }
}
