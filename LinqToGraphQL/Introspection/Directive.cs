using System;
using System.Linq;
using LinqToGraphQL.Builders;

namespace LinqToGraphQL.Introspection
{
    public class Directive : QueryEntity
    {
        public Directive(IQueryProvider provider)
            : base(provider)
        {
        }

        public string Name { get; }
        public string Description { get; }
        public IQueryable<DirectiveLocation> Locations => this.CreateProperty(x => x.Locations);
        public IQueryable<InputValue> Args => this.CreateProperty(x => x.Args);
    }
}
