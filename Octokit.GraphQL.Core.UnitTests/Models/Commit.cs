using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class Commit : QueryableValue<Commit>
    {
        public Commit(Expression expression) : base(expression)
        {
        }

        public string AbbreviatedOid { get; }
        public CheckSuiteConnection CheckSuites(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<CheckSuiteFilter>? filterBy = null) => this.CreateMethodCall(x => x.CheckSuites(first, after, last, before, filterBy), CheckSuiteConnection.Create);

        internal static Commit Create(Expression expression)
        {
            return new Commit(expression);
        }
    }
}