using System;
using System.Linq;
using LinqToGraphQL.Builders;

namespace LinqToGraphQL.Introspection
{
    public class InputValue : QueryEntity
    {
        public InputValue(IQueryProvider provider)
            : base(provider)
        {
            Type = this.CreateProperty(x => x.Type, SchemaType.Create);
        }

        public string Name { get; }
        public string Description { get; }
        public SchemaType Type { get; }
        public string DefaultValue { get; }
    }
}
