using System;
using System.Linq;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class NestedQuery : QueryableValue
    {
        public NestedQuery(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public IQueryableValue<Simple> Simple(string arg1)
        {
            return this.CreateMethodCall(x => x.Simple(arg1));
        }

        internal static NestedQuery Create(IQueryProvider provider, Expression expression)
        {
            return new NestedQuery(provider, expression);
        }
    }
}
