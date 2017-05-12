using System;
using System.Linq;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class Union : QueryEntity, IUnion
    {
        public Union(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public Simple Simple => this.CreateProperty(x => x.Simple, Simple.Create);

        public NestedData Nested => this.CreateProperty(x => x.Nested, Nested.Create);
    }
}
