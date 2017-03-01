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
    public class GraphQLQuery<TResult>
    {
        public GraphQLQuery(
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

        public override string ToString()
        {
            return new QuerySerializer(2).Serialize(OperationDefinition);
        }

        public string GetPayload()
        {
            var payload = new
            {
                Query = new QuerySerializer().Serialize(OperationDefinition),
            };

            return JsonConvert.SerializeObject(
                payload,
                Formatting.None,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
