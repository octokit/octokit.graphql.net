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
        }

        public string Id { get; set; }
        public IQueryable<Simple> Items => this.CreateProperty(x => x.Items);
    }
}
