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
        }

        public string Name { get; }
        public string Description { get; }
        public IQueryable<InputValue> Args => this.CreateProperty(x => x.Args);
        public SchemaType Type => this.CreateProperty(x => x.Type, SchemaType.Create);
        public bool IsDeprecated { get; }
        public string DeprecationReason { get; }
    }
}
