using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToGraphQL.UnitTests.Models
{
    class NestedData
    {
        public string Id { get; set; }
        public IEnumerable<Simple> Items { get; set; }
    }
}
