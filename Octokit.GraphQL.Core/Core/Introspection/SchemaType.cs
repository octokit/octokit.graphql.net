using System;
using System.Linq;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.Introspection
{
    public class SchemaType : QueryableValue, IQuery, IQueryableValue<SchemaType>
    {
        public SchemaType(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public TypeKind Kind { get; }
        public string Name { get; }
        public string Description { get; }
        public IQueryable<SchemaType> Interfaces => this.CreateProperty(x => Interfaces);
        public IQueryable<SchemaType> PossibleTypes => this.CreateProperty(x => x.PossibleTypes);
        public IQueryable<InputValue> InputFields => this.CreateProperty(x => x.InputFields);
        public SchemaType OfType => this.CreateProperty(x => x.OfType, SchemaType.Create);

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
