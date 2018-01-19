using System;
using System.Linq;
using System.Reflection;

namespace Octokit.GraphQL.Core.Builders
{
    internal static class LinqMethods
    {
        public static readonly MethodInfo ToListMethod = typeof(Enumerable).GetTypeInfo().GetDeclaredMethod(nameof(Enumerable.ToList));
    }
}
