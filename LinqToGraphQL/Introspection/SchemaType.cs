using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL.Builders;

namespace LinqToGraphQL.Introspection
{
    public class SchemaType : QueryEntity, IRootQuery
    {
        public SchemaType(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
            Interfaces = this.CreateProperty(x => Interfaces);
            PossibleTypes = this.CreateProperty(x => x.PossibleTypes);
            InputFields = this.CreateProperty(x => x.InputFields);
            OfType = this.CreateProperty(x => x.OfType, SchemaType.Create);
        }

        public TypeKind Kind { get; }
        public string Name { get; }
        public string Description { get; }
        public IQueryable<SchemaType> Interfaces { get; }
        public IQueryable<SchemaType> PossibleTypes { get; }
        public IQueryable<InputValue> InputFields { get; }
        public SchemaType OfType { get; }

        public IQueryable<Field> Fields(bool includeDeprecated = false)
        {
            return this.CreateMethodCall(x => x.Fields(includeDeprecated));
        }

        public IQueryable<EnumValue> EnumValues(bool includeDeprecated = false)
        {
            return this.CreateMethodCall(x => x.EnumValues(includeDeprecated));
        }

        internal static SchemaType Create(IQueryProvider provider, Expression expression)
        {
            return new SchemaType(provider, expression);
        }
    }
}
