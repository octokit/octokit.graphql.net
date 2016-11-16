using System;
using System.Collections.Generic;
using System.Reflection;

namespace LinqToGraphQL.Syntax
{
    public class FieldSelection : SelectionSet
    {
        public FieldSelection(ISelectionSet parent, MemberInfo member)
            : this(parent, GetReturnType(member), GetIdentifier(member))
        {
        }

        public FieldSelection(ISelectionSet parent, Type type, string name)
        {
            Name = name;
            ResultType = type;
            Arguments = new List<Argument>();
            parent.Selections.Add(this);
        }

        public string Name { get; }
        public IList<Argument> Arguments { get; }

        private static Type GetReturnType(MemberInfo member)
        {
            var property = member as PropertyInfo;
            return property?.PropertyType;
        }
    }
}
