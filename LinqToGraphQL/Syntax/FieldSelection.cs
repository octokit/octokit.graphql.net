using System;
using System.Collections.Generic;
using System.Reflection;

namespace LinqToGraphQL.Syntax
{
    public class FieldSelection : SelectionSet
    {
        public FieldSelection(ISelectionSet parent, MemberInfo member)
            : this(parent, GetIdentifier(member))
        {
        }

        public FieldSelection(ISelectionSet parent, string name)
        {
            Name = name;
            Arguments = new List<Argument>();
            parent.Selections.Add(this);
        }

        public string Name { get; }
        public IList<Argument> Arguments { get; }
    }
}
