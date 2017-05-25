using System;
using System.Linq;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class RootQuery : QueryEntity, IRootQuery
    {
        public RootQuery()
            : base(new QueryProvider())
        {
        }

        public IQueryable<NestedData> Data => this.CreateProperty(x => Data);

        public IQueryable<Union> Union => this.CreateProperty(x => Union);

        public IQueryable<Simple> Simple(string arg1, int? arg2 = null)
        {
            return this.CreateMethodCall(x => x.Simple(arg1, arg2));
        }

        public NestedQuery Nested(string arg1, int? arg2 = null)
        {
            return this.CreateMethodCall(x => x.Nested(arg1, arg2), NestedQuery.Create);
        }

        public IQueryable<Simple> StringValue(string value)
        {
            return this.CreateMethodCall(x => x.StringValue(value));
        }

        public IQueryable<Simple> BoolValue(bool boolean)
        {
            return this.CreateMethodCall(x => x.BoolValue(boolean));
        }

        public IQueryable<Simple> IntValue(int integer)
        {
            return this.CreateMethodCall(x => x.IntValue(integer));
        }

        public IQueryable<Simple> FloatValue(float flt)
        {
            return this.CreateMethodCall(x => x.FloatValue(flt));
        }

        public IQueryable<Simple> ObjectValue(object value)
        {
            return this.CreateMethodCall(x => x.ObjectValue(value));
        }
    }
}
