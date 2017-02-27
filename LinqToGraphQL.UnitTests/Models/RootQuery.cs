using System;
using System.Linq;
using LinqToGraphQL.Builders;

namespace LinqToGraphQL.UnitTests.Models
{
    class RootQuery : QueryEntity, IRootQuery
    {
        public RootQuery()
            : base(new QueryProvider())
        {
            Data = this.CreateProperty(x => Data);
        }

        public IQueryable<NestedData> Data { get; }

        public IQueryable<Simple> Simple(string arg1, int? arg2 = null)
        {
            return this.CreateMethodCall(x => x.Simple(arg1, arg2));
        }

        public NestedQuery Nested(string arg1, int? arg2 = null)
        {
            return this.CreateMethodCall(x => x.Nested(arg1, arg2), NestedQuery.Create);
        }

        public IQueryable<Simple> Another(bool boolean)
        {
            return this.CreateMethodCall(x => x.Another(boolean));
        }
    }
}
