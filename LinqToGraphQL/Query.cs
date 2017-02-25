using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using LinqToGraphQL.Serializers;
using LinqToGraphQL.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace LinqToGraphQL
{
    public class Query<TResult>
    {
        public Query(
            OperationDefinition operationDefinition,
            Expression<Func<JObject,
                IEnumerable<TResult>>> expression)
        {
            OperationDefinition = operationDefinition;
            Expression = expression;
            CompiledExpression = expression.Compile();
        }

        public OperationDefinition OperationDefinition { get; }

        public Expression<Func<JObject, IEnumerable<TResult>>> Expression { get; }

        public Func<JObject, IEnumerable<TResult>> CompiledExpression { get; }

        public override string ToString() => ToString(true);

        public string ToString(bool indented)
        {
            var payload = new
            {
                Query = new QuerySerializer(indented ? 2 : 0).Serialize(OperationDefinition),
            };

            return JsonConvert.SerializeObject(
                payload,
                indented ? Formatting.Indented : Formatting.None,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
