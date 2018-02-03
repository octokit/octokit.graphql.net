using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class Simple : QueryableValue<Simple>, INode
    {
        public Simple(Expression expression)
            : base(expression)
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }
        public int? NullableNumber { get; set; }

        public IPagedList<NestedDataConnection> PagesOfNested(Arg<PageOption>? option = null)
        {
            return this.CreateMethodCall(x => x.PagesOfNested(option));
        }

        [GraphQLIdentifier("pagesOfNested")]
        internal NestedDataConnection PagesOfNestedInternal(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<PageOption>? option = null)
        {
            return this.CreateMethodCall(x => x.PagesOfNestedInternal(first, after, last, before, option), NestedDataConnection.Create);
        }

        internal static Simple Create(Expression expression)
        {
            return new Simple(expression);
        }
    }
}
