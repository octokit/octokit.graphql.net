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
            Locations = this.CreateProperty(x => x.Locations);
            Args = this.CreateProperty(x => x.Args);
        }

        public string Name { get; }
        public string Description { get; }
        public IQueryable<DirectiveLocation> Locations { get; }
        public IQueryable<InputValue> Args { get; }
    }
}
