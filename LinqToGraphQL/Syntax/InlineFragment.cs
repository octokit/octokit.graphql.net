using System;
using System.Reflection;

namespace LinqToGraphQL.Syntax
{
    public class InlineFragment : SelectionSet
    {
        public InlineFragment(Type typeCondition)
        {
            TypeCondition = typeCondition;
        }

        public Type TypeCondition { get; }
    }
}
