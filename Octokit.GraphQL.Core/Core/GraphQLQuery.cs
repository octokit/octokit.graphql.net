using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Serializers;
using Octokit.GraphQL.Core.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Octokit.GraphQL.Core
{
    public class GraphQLQuery<TResult>
    {
        public GraphQLQuery(
            OperationDefinition operationDefinition,
            Expression<Func<JObject, TResult>> expression)
        {
            OperationDefinition = operationDefinition;
            Expression = expression;
            CompiledExpression = expression.Compile();
        }

        public OperationDefinition OperationDefinition { get; }

        public Expression<Func<JObject, TResult>> Expression { get; }

        public Func<JObject, TResult> CompiledExpression { get; }

        public override string ToString()
        {
            return new QuerySerializer(2).Serialize(OperationDefinition);
        }

        public string GetPayload()
        {
            var query = new QuerySerializer().Serialize(OperationDefinition);
            var payload = new
            {
                Query = query,
            };

            return JsonConvert.SerializeObject(
                payload,
                Formatting.None,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
    }
}
