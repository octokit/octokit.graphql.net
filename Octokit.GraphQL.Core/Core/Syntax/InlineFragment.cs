using System;
using System.Reflection;

namespace Octokit.GraphQL.Core.Syntax
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
