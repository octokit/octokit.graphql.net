using System;
using System.Linq;
using LinqToGraphQL.Builders;

namespace LinqToGraphQL.Introspection
{
    public class Schema : QueryEntity, IRootQuery
    {
        public Schema(IQueryProvider provider)
            : base(provider)
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
    }
}
