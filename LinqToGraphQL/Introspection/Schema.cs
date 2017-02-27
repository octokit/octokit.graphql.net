using System;
using System.Linq;
using System.Linq.Expressions;
using LinqToGraphQL.Builders;

namespace LinqToGraphQL.Introspection
{
    public class Schema : QueryEntity
    {
        public Schema(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
            Types = this.CreateProperty(x => x.Types);
            QueryType = this.CreateProperty(x => x.QueryType, SchemaType.Create);
            MutationType = this.CreateProperty(x => x.MutationType, SchemaType.Create);
            Directives = this.CreateProperty(x => x.Directives);
        }

        public IQueryable<SchemaType> Types { get; }
        public SchemaType QueryType { get; }
        public SchemaType MutationType { get; }
        public IQueryable<Directive> Directives { get; }

        internal static Schema Create(IQueryProvider provider, Expression expression)
        {
            return new Schema(provider, expression);
        }
    }
}
