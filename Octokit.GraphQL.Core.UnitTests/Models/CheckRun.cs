using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class CheckRun : QueryableValue<CheckRun>
    {
        public CheckRun(Expression expression) : base(expression)
        {
        }

        public CheckAnnotationConnection Annotations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Annotations(first, after, last, before), CheckAnnotationConnection.Create);

        public CheckSuite CheckSuite => this.CreateProperty(x => x.CheckSuite, Models.CheckSuite.Create);

        public string Id { get; }

        public string Name { get; }

        internal static CheckRun Create(Expression expression)
        {
            return new CheckRun(expression);
        }
    }
}