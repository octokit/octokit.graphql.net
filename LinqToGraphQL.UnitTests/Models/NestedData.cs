using System;
using System.Linq;

namespace LinqToGraphQL.UnitTests.Models
{
    class NestedData : QueryEntity
    {
        public NestedData(IQueryProvider provider)
            : base(provider)
        {
        }

        public string Id { get; set; }
        public IQueryable<Simple> Items { get; set; }
    }
}
