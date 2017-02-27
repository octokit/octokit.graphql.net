using System;

namespace LinqToGraphQL.Introspection
{
    public enum TypeKind
    {
        [GraphQLIdentifier("SCALAR")]
        Scalar,
        [GraphQLIdentifier("OBJECT")]
        Object,
        [GraphQLIdentifier("UNION")]
        Union,
        [GraphQLIdentifier("INTERFACE")]
        Interface,
        [GraphQLIdentifier("ENUM")]
        Enum,
        [GraphQLIdentifier("INPUT_OBJECT")]
        InputObject,
        [GraphQLIdentifier("LIST")]
        List,
        [GraphQLIdentifier("NON_NULL")]
        NonNull
    }
}
