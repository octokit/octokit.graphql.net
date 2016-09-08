using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToGraphQL.UnitTests.Models
{
    class RootQuery : QueryEntity, IRootQuery
    {
        public RootQuery()
            : base(new QueryProvider())
        {
        }

        public IQueryable<SimpleQuery> Simple(string arg1, int? arg2 = null)
        {
            return MethodCall<SimpleQuery>(nameof(Simple), Arg(arg1), Arg(arg2));
        }
    }
}
