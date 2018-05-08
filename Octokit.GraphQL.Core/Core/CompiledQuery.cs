using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Serializers;
using Octokit.GraphQL.Core.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Octokit.GraphQL.Core;

namespace Octokit.GraphQL.Core
{
    public class CompiledQuery<TResult> : ICompiledQuery<TResult>
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
            return ToString(2);
        }

        public string ToString(int indentation)
        {
            return new QuerySerializer(indentation).Serialize(OperationDefinition);
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

        public IQueryRunner<TResult> Start(IConnection connection, Dictionary<string, object> variables)
        {
            return new Runner(this, connection, variables);
        }

        IQueryRunner ICompiledQuery.Start(IConnection connection, Dictionary<string, object> variables)
        {
            return Start(connection, variables);
        }

        class Runner : IQueryRunner<TResult>
        {
            readonly CompiledQuery<TResult> parent;
            readonly IConnection connection;
            readonly Dictionary<string, object> variables;

            public Runner(
                CompiledQuery<TResult> parent,
                IConnection connection,
                Dictionary<string, object> variables)
            {
                this.parent = parent;
                this.connection = connection;
                this.variables = variables;
            }

            public TResult Result { get; private set; }
            object IQueryRunner.Result => Result;

            public async Task<bool> RunPage()
            {
                Result = await connection.Run(parent.GetPayload(variables), parent.CompiledExpression);
                return false;
            }
        }
    }
}
