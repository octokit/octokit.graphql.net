using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Serializers;
using Octokit.GraphQL.Core.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core
{
    public class SimpleQuery<TResult> : ICompiledQuery<TResult>
    {
        public SimpleQuery(
            OperationDefinition operationDefinition,
            Expression<Func<JObject, TResult>> resultBuilder)
        {
            var serializer = new QuerySerializer();
            OperationDefinition = operationDefinition;
            Query = serializer.Serialize(operationDefinition);
            ResultBuilder = ExpressionCompiler.Compile(resultBuilder);
        }

        public OperationDefinition OperationDefinition { get; }

        public string Query { get; }

        public Func<JObject, TResult> ResultBuilder { get; }

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

        public IQueryRunner<TResult> Start(IConnection connection, IDictionary<string, object> variables)
        {
            return new Runner(this, connection, variables);
        }

        IQueryRunner ICompiledQuery.Start(IConnection connection, IDictionary<string, object> variables)
        {
            return Start(connection, variables);
        }

        class Runner : IQueryRunner<TResult>
        {
            readonly SimpleQuery<TResult> parent;
            readonly IConnection connection;
            readonly IDictionary<string, object> variables;

            public Runner(
                SimpleQuery<TResult> parent,
                IConnection connection,
                IDictionary<string, object> variables)
            {
                this.parent = parent;
                this.connection = connection;
                this.variables = variables;
            }

            public TResult Result { get; private set; }
            object IQueryRunner.Result => Result;

            public async Task<bool> RunPage()
            {
                Result = await connection.Run(parent.GetPayload(variables), parent.ResultBuilder);
                return false;
            }
        }
    }
}
