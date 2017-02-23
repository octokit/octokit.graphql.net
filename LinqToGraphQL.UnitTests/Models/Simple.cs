using System;
using System.Linq;

namespace LinqToGraphQL.UnitTests.Models
{
    class Simple : QueryEntity
    {
        public Simple(IQueryProvider provider)
            : base(provider)
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
