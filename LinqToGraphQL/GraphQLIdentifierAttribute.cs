using System;

namespace LinqToGraphQL
{
    public class GraphQLIdentifierAttribute : Attribute
    {
        public GraphQLIdentifierAttribute(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }
    }
}
