using System;
using System.Linq;

namespace LinqToGraphQL.UnitTests.Models
{
    class RootQuery : QueryEntity, IRootQuery
    {
        public RootQuery()
            : base(new QueryProvider())
        {
        }

        public IQueryable<NestedData> Data
        {
            get { return Property<NestedData>(nameof(Data)); }
        }

        public IQueryable<Simple> Simple(string arg1, int? arg2 = null)
        {
            return MethodCall<Simple>(nameof(Simple), Arg(arg1), Arg(arg2));
        }

        public NestedQuery Nested(string arg1, int? arg2 = null)
        {
            var expression = MethodCallExpression(nameof(Nested), Arg(arg1), Arg(arg2));
            return new NestedQuery(Provider, expression);
        }
    }
}
