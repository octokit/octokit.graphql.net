using System;
using System.Linq;
using LinqToGraphQL.Builders;

namespace LinqToGraphQL.UnitTests.Models
{
    class NestedData : QueryEntity
    {
        public NestedData(IQueryProvider provider)
            : base(provider)
        {
            Items = this.CreateProperty(x => x.Items);
        }

        public string Id { get; set; }
        public IQueryable<Simple> Items { get; }
    }
}
