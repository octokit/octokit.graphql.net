using System;
using System.Collections.Generic;
using System.Reflection;

namespace LinqToGraphQL.Syntax
{
    public class FieldSelection : SelectionSet
    {
        public FieldSelection(MemberInfo member)
            : this(GetReturnType(member), GetIdentifier(member))
        {
        }

        public FieldSelection(Type type, string name)
        {
            Name = name;
            ResultType = type;
            Arguments = new List<Argument>();
        }

        public string Name { get; }
        public IList<Argument> Arguments { get; }
        public string Alias { get; set; }

        private static Type GetReturnType(MemberInfo member)
        {
            var property = member as PropertyInfo;
            return property?.PropertyType;
        }
    }
}
