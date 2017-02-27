using System;
using System.Linq;
using LinqToGraphQL.Builders;

namespace LinqToGraphQL.Introspection
{
    public class Field : QueryEntity
    {
        public Field(IQueryProvider provider)
            : base(provider)
        {
            Args = this.CreateProperty(x => x.Args);
            Type = this.CreateProperty(x => x.Type, SchemaType.Create);
        }

        public string Name { get; }
        public string Description { get; }
        public IQueryable<InputValue> Args { get; }
        public SchemaType Type { get; }
        public bool IsDeprecated { get; }
        public string DeprecationReason { get; }
    }
}
