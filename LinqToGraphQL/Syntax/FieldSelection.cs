using System;
using System.Collections.Generic;
using System.Reflection;

namespace LinqToGraphQL.Syntax
{
    public class FieldSelection : SelectionSet
    {
        public FieldSelection(MemberInfo member, MemberInfo alias)
            : this(GetReturnType(member), GetIdentifier(member), GetIdentifier(alias))
        {
        }

        public FieldSelection(Type type, string name, string alias)
        {
            Name = name;
            ResultType = type;
            Arguments = new List<Argument>();
            Alias = alias != name ? alias : null;
        }

        public string Name { get; }
        public IList<Argument> Arguments { get; }
        public string Alias { get; private set; }

        public void SetAlias(MemberInfo member)
        {
            var alias = GetIdentifier(member);

            if (Name != alias)
            {
                Alias = alias;
            }
        }

        private static Type GetReturnType(MemberInfo member)
        {
            var property = member as PropertyInfo;
            return property?.PropertyType;
        }
    }
}
