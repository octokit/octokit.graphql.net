using System;

namespace LinqToGraphQL.Introspection
{
    public enum DirectiveLocation
    {
        Query,
        Mutation,
        Field,
        FragmentDefinition,
        FragmentSpread,
        InlineFragment,
    }
}
