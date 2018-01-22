using System;
using System.Collections.Generic;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class TestQuery : QueryableValue<TestQuery>, IQuery
    {
        public TestQuery()
            : base(null)
        {
        }

        public IQueryableList<NestedData> Data => this.CreateProperty(x => Data);

        public Union Union => this.CreateProperty(x => Union, Union.Create);

        public Simple Simple(string arg1, int? arg2 = null)
        {
            return this.CreateMethodCall(x => x.Simple(arg1, arg2), Models.Simple.Create);
        }

        public NestedQuery Nested(string arg1, int? arg2 = null)
        {
            return this.CreateMethodCall(x => x.Nested(arg1, arg2), NestedQuery.Create);
        }

        public Simple StringValue(string value)
        {
            return this.CreateMethodCall(x => x.StringValue(value), Models.Simple.Create);
        }

        public Simple BoolValue(bool boolean)
        {
            return this.CreateMethodCall(x => x.BoolValue(boolean), Models.Simple.Create);
        }

        public Simple IntValue(int integer)
        {
            return this.CreateMethodCall(x => x.IntValue(integer), Models.Simple.Create);
        }

        public Simple FloatValue(float flt)
        {
            return this.CreateMethodCall(x => x.FloatValue(flt), Models.Simple.Create);
        }

        public Simple ObjectValue(object value)
        {
            return this.CreateMethodCall(x => x.ObjectValue(value), Models.Simple.Create);
        }

        public Simple EnumerableValue(IEnumerable<object> value)
        {
            return this.CreateMethodCall(x => x.EnumerableValue(value), Models.Simple.Create);
        }

        public Simple InputObject(InputObject input)
        {
            return this.CreateMethodCall(x => x.InputObject(input), Models.Simple.Create);
        }
    }
}
