using System;
using System.Linq;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.Introspection
{
    public class Directive : QueryableValue
    {
        public Directive(IQueryProvider provider)
            : base(provider)
        {
        }

        public string Name { get; }
        public string Description { get; }
        public DirectiveLocation Locations { get; }
        public IQueryableList<InputValue> Args => this.CreateProperty(x => x.Args);
    }
}
