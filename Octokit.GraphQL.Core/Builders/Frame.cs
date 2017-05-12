using System;
using System.Reflection;
using GraphQLParser.AST;

namespace LinqToGraphQL.Builders
{
    internal class Frame
    {
        public Frame(ASTNode node)
        {
            Node = node;
        }

        public ASTNode Node { get; }

        protected static string GetIdentifier(MemberInfo member)
        {
            var attr = member.GetCustomAttribute<GraphQLIdentifierAttribute>();
            return attr != null ? attr.Identifier : member.Name.ToCamelCase();
        }
    }
}
