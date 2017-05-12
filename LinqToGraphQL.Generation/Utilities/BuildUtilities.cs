using System;
using System.Collections.Generic;
using System.Linq;
using Octokit.GraphQL.Core.Generation.Models;
using Octokit.GraphQL.Core.Introspection;

namespace Octokit.GraphQL.Core.Generation.Utilities
{
    internal static class BuildUtilities
    {
        public static IEnumerable<InputValueModel> SortArgs(IEnumerable<InputValueModel> args)
        {
            return args.OrderBy(x => x, new DefaultValueComparer());
        }

        private class DefaultValueComparer : IComparer<InputValueModel>
        {
            public int Compare(InputValueModel x, InputValueModel y)
            {
                return DefaultValueWeight(x) - DefaultValueWeight(y);
            }

            private static int DefaultValueWeight(InputValueModel i)
            {
                return i.DefaultValue != null || i.Type.Kind != TypeKind.NonNull ? 1 : -1;
            }
        }
    }
}
