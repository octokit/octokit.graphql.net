using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class NestedQuery : QueryableValue<NestedQuery>
    {
        public NestedQuery(Expression expression)
            : base(expression)
        {
        }

        public Simple Simple(string arg1)
        {
            return this.CreateMethodCall(x => x.Simple(arg1), Models.Simple.Create);
        }

        internal static NestedQuery Create(Expression expression)
        {
            return new NestedQuery(expression);
        }
    }
}
