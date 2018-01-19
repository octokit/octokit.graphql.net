using System;
using System.Linq;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.Introspection
{
    public class Schema : QueryableValue, IQueryableValue<Schema>
    {
        public Schema(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public IQueryableList<SchemaType> Types => this.CreateProperty(x => x.Types);
        public SchemaType QueryType => this.CreateProperty(x => x.QueryType, SchemaType.Create);
        public SchemaType MutationType => this.CreateProperty(x => x.MutationType, SchemaType.Create);
        public IQueryableList<Directive> Directives => this.CreateProperty(x => x.Directives);
        public Schema Value { get; }

        internal static Schema Create(IQueryProvider provider, Expression expression)
        {
            return new Schema(provider, expression);
        }
    }
}
