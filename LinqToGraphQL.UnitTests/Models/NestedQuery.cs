using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToGraphQL.UnitTests.Models
{
    class NestedQuery : QueryEntity
    {
        public NestedQuery(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public IQueryable<Simple> Simple(string arg1)
        {
            return MethodCall<Simple>(nameof(Simple), Arg(arg1));
        }
    }
}
