using System;
using System.Reflection;

namespace LinqToGraphQL.Syntax
{
    public class InlineFragment : SelectionSet
    {
        public InlineFragment(ISelectionSet parent, Type typeCondition)
        {
            TypeCondition = typeCondition;
            parent.Selections.Add(this);
        }

        public Type TypeCondition { get; }
    }
}
