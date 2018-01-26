using System;
using System.Linq;
using System.Reflection;

namespace Octokit.GraphQL.Core.Builders
{
    internal static class LinqMethods
    {
        public static readonly MethodInfo ToDictionaryMethod = typeof(Enumerable)
            .GetTypeInfo()
            .GetDeclaredMethods(nameof(Enumerable.ToDictionary))
            .Single(x => x.GetGenericArguments().Length == 3 && x.GetParameters().Length == 3);
        public static readonly MethodInfo ToListMethod = typeof(Enumerable).GetTypeInfo().GetDeclaredMethod(nameof(Enumerable.ToList));
    }
}
