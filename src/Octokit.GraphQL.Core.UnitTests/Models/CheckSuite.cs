using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class CheckSuite : QueryableValue<CheckSuite>
    {
        public CheckSuite(Expression expression) : base(expression)
        {
        }

        public CheckRunConnection CheckRuns(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<CheckRunFilter>? filterBy = null) => this.CreateMethodCall(x => x.CheckRuns(first, after, last, before, filterBy), CheckRunConnection.Create);

        internal static CheckSuite Create(Expression expression)
        {
            return new CheckSuite(expression);
        }
    }
}