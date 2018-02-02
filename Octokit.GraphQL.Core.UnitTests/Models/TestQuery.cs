using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;
using static Octokit.GraphQL.Variable;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class TestQuery : QueryableValue<TestQuery>, IRootQuery
    {
        public TestQuery()
            : base(null)
        {
        }

        public IQueryableList<NestedData> QueryItems => this.CreateProperty(x => QueryItems);

        public Union Union => this.CreateProperty(x => Union, Union.Create);

        public Simple Simple(Arg<string> arg1, Arg<int>? arg2 = null)
        {
            return this.CreateMethodCall(x => x.Simple(arg1, arg2), Models.Simple.Create);
        }

        public NestedQuery Nested(Arg<string> arg1, Arg<int>? arg2 = null)
        {
            return this.CreateMethodCall(x => x.Nested(arg1, arg2), NestedQuery.Create);
        }

        public Simple StringValue(Arg<string> value)
        {
            return this.CreateMethodCall(x => x.StringValue(value), Models.Simple.Create);
        }

        public Simple BoolValue(Arg<bool> boolean)
        {
            return this.CreateMethodCall(x => x.BoolValue(boolean), Models.Simple.Create);
        }

        public Simple IntValue(Arg<int> integer)
        {
            return this.CreateMethodCall(x => x.IntValue(integer), Models.Simple.Create);
        }

        public Simple FloatValue(Arg<float> flt)
        {
            return this.CreateMethodCall(x => x.FloatValue(flt), Models.Simple.Create);
        }

        public Simple ObjectValue(Arg<object> value)
        {
            return this.CreateMethodCall(x => x.ObjectValue(value), Models.Simple.Create);
        }

        public Simple EnumerableValue(Arg<IEnumerable<object>> value)
        {
            return this.CreateMethodCall(x => x.EnumerableValue(value), Models.Simple.Create);
        }

        public Simple InputObject(Arg<InputObject> input)
        {
            return this.CreateMethodCall(x => x.InputObject(input), Models.Simple.Create);
        }

        public INode Node(int id)
        {
            return this.CreateMethodCall(x => x.Node(id), Models.StubINode.Create);
        }

        public NestedDataConnection PagesOfNested(Arg<int>? first = null, Arg<string>? after = null, Arg<PageOption>? option = null)
        {
            return this.CreateMethodCall(x => x.PagesOfNested(first, after, option), NestedDataConnection.Create);
        }

        public IPagedList<NestedDataConnection> PagesOfNested(Arg<PageOption>? option = null)
        {
            return this.CreatePagedMethodCall(x => x.PagesOfNested(new Arg<int>("first", 0), new Arg<string>("after", null), option));
        }
    }

    static class TestQueryPaging
    {
    }
}
