using System;
using System.Collections.Generic;
using System.Reflection;
using Octokit.GraphQL.Core.Utilities;

namespace Octokit.GraphQL.Core.Syntax
{
    public class SelectionSet : ISelectionSet
    {
        private static readonly IEnumerable<ISyntaxNode> EmptySelections = new ISyntaxNode[0];

        public SelectionSet()
        {
            Selections = new List<ISyntaxNode>();
        }

        public IList<ISyntaxNode> Selections { get; }

        public static string GetIdentifier(MemberInfo member)
        {
            var attr = member?.GetCustomAttribute<GraphQLIdentifierAttribute>();
            return attr != null ? attr.Identifier : member?.Name.LowerFirstCharacter();
        }
    }
}
