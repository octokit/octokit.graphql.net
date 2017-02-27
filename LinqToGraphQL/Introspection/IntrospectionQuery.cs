using System;
using LinqToGraphQL.Builders;

namespace LinqToGraphQL.Introspection
{
    public class IntrospectionQuery : QueryEntity, IRootQuery
    {
        public IntrospectionQuery()
            : base(new QueryProvider())
        {
            Schema = this.CreateProperty(x => x.Schema, Schema.Create);
        }

        [GraphQLIdentifier("__schema")]
        public Schema Schema { get; }
    }
}
