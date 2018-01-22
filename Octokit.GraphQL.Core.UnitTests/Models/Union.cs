using System;
using System.Linq;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class Union : QueryableValue<Union>, IUnion
    {
        public Union(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public Simple Simple => this.CreateProperty(x => x.Simple, Simple.Create);

        public NestedData Nested => this.CreateProperty(x => x.Nested, Nested.Create);

        internal static Union Create(IQueryProvider provider, Expression expression)
        {
            return new Union(provider, expression);
        }
    }
}
