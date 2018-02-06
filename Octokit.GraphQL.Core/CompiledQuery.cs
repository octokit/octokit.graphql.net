using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Serializers;
using Octokit.GraphQL.Core.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Octokit.GraphQL
{
    public class CompiledQuery<TResult>
    {
        public CompiledQuery(
            OperationDefinition operationDefinition,
            Expression<Func<JObject, TResult>> expression)
        {
            var serializer = new QuerySerializer();
            OperationDefinition = operationDefinition;
            Query = serializer.Serialize(operationDefinition);
            Expression = expression;
            CompiledExpression = expression.Compile();
        }

        public OperationDefinition OperationDefinition { get; }

        public string Query { get; }

        public Expression<Func<JObject, TResult>> Expression { get; }

        public Func<JObject, TResult> CompiledExpression { get; }

        public override string ToString()
        {
            return new QuerySerializer(2).Serialize(OperationDefinition);
        }

        public string GetPayload(IDictionary<string, object> variables)
        {
           var payload = new
            {
                Query,
                Variables = variables,
            };

            return JsonConvert.SerializeObject(
                payload,
                Formatting.None,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
