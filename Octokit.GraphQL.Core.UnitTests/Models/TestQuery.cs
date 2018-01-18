using System;
using System.Collections.Generic;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class TestQuery : QueryableValue, IQuery
    {
        public TestQuery()
            : base(new QueryProvider())
        {
        }

        public IQueryableList<NestedData> Data => this.CreateProperty(x => Data);

        public IQueryableValue<Union> Union => this.CreateProperty(x => Union);

        public IQueryableValue<Simple> Simple(string arg1, int? arg2 = null)
        {
            return this.CreateMethodCall(x => x.Simple(arg1, arg2));
        }

        public NestedQuery Nested(string arg1, int? arg2 = null)
        {
            return this.CreateMethodCall(x => x.Nested(arg1, arg2), NestedQuery.Create);
        }

        public IQueryableValue<Simple> StringValue(string value)
        {
            return this.CreateMethodCall(x => x.StringValue(value));
        }

        public IQueryableValue<Simple> BoolValue(bool boolean)
        {
            return this.CreateMethodCall(x => x.BoolValue(boolean));
        }

        public IQueryableValue<Simple> IntValue(int integer)
        {
            return this.CreateMethodCall(x => x.IntValue(integer));
        }

        public IQueryableValue<Simple> FloatValue(float flt)
        {
            return this.CreateMethodCall(x => x.FloatValue(flt));
        }

        public IQueryableValue<Simple> ObjectValue(object value)
        {
            return this.CreateMethodCall(x => x.ObjectValue(value));
        }

        public IQueryableValue<Simple> EnumerableValue(IEnumerable<object> value)
        {
            return this.CreateMethodCall(x => x.EnumerableValue(value));
        }

        public IQueryableValue<Simple> InputObject(InputObject input)
        {
            return this.CreateMethodCall(x => x.InputObject(input));
        }
    }
}
