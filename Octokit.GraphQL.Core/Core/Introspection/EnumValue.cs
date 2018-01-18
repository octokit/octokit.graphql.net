using System;
using System.Linq;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.Introspection
{
    public class EnumValue : QueryableValue
    {
        public EnumValue(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public string Name { get; }
        public string Description { get; }
        public bool IsDeprecated { get; }
        public string DeprecationReason { get; }

        internal static EnumValue Create(IQueryProvider provider, Expression expression)
        {
            return new EnumValue(provider, expression);
        }
    }
}
