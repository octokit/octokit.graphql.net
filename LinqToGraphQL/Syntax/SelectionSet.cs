using System;
using System.Collections.Generic;
using System.Reflection;
using LinqToGraphQL.Utilities;

namespace LinqToGraphQL.Syntax
{
    public class SelectionSet : ISelectionSet
    {
        private static readonly IEnumerable<ISyntaxNode> EmptySelections = new ISyntaxNode[0];

        public SelectionSet()
        {
            Selections = new List<ISyntaxNode>();
        }

        public Type ResultType { get; set; }
        public IList<ISyntaxNode> Selections { get; }

        public static string GetIdentifier(MemberInfo member)
        {
            var attr = member?.GetCustomAttribute<GraphQLIdentifierAttribute>();
            return attr != null ? attr.Identifier : member?.Name.ToCamelCase();
        }
    }
}
