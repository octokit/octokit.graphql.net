using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.Deserializers;
using Octokit.GraphQL.Core.Syntax;

namespace Octokit.GraphQL.Core
{
    public class SimpleSubquery<TResult> : SimpleQuery<TResult>, ISubquery
    {
        public SimpleSubquery(
            OperationDefinition operationDefinition,
            Expression<Func<JObject, TResult>> expression,
            Expression<Func<JObject, IEnumerable<JToken>>> parentIds,
            Expression<Func<JObject, JToken>> pageInfo,
            Expression<Func<JObject, IEnumerable<JToken>>> parentPageInfo)
            : base(operationDefinition, expression)
        {
            ParentIds = ExpressionCompiler.Compile(parentIds);
            PageInfo = ExpressionCompiler.Compile(pageInfo);
            ParentPageInfo = ExpressionCompiler.Compile(parentPageInfo);
        }

        public Func<JObject, IEnumerable<JToken>> ParentIds { get; }
        public Func<JObject, JToken> PageInfo { get; }
        public Func<JObject, IEnumerable<JToken>> ParentPageInfo { get; }

        public IQueryRunner Start(
            IConnection connection,
            string id,
            string after,
            IDictionary<string, object> variables,
            IList result)
        {
            return new Runner(this, connection, id, after, variables, result);
        }

        public static ISubquery Create(
            Type resultType,
            OperationDefinition operationDefinition,
            Expression expression,
            Expression<Func<JObject, IEnumerable<JToken>>> parentIds,
            Expression<Func<JObject, JToken>> pageInfo,
            Expression<Func<JObject, IEnumerable<JToken>>> parentPageInfo)
        {
            var ctor = typeof(SimpleSubquery<>)
                .MakeGenericType(resultType)
                .GetTypeInfo()
                .DeclaredConstructors
                .Single();

            return (ISubquery)ctor.Invoke(new object[]
            {
                operationDefinition,
                expression,
                parentIds,
                pageInfo,
                parentPageInfo
            });
        }

        class Runner : IQueryRunner<TResult>
        {
            readonly SimpleSubquery<TResult> owner;
            readonly IConnection connection;
            readonly Dictionary<string, object> variables;
            readonly ResponseDeserializer deserializer = new ResponseDeserializer();
            IList finalResult;

            public Runner(
               SimpleSubquery<TResult> owner,
               IConnection connection,
               string id,
               string after,
               IDictionary<string, object> variables,
               IList result)
            {
                this.owner = owner;
                this.connection = connection;
                this.variables = variables?.ToDictionary(x => x.Key, x => x.Value) ?? 
                    new Dictionary<string, object>();
                this.variables["__id"] = id;
                this.variables["__after"] = after;
                finalResult = result;
            }

            public TResult Result { get; private set; }

            object IQueryRunner.Result => Result;

            public async Task<bool> RunPage()
            {
                var payload = owner.GetPayload(variables);
                var data = await connection.Run(payload);
                var json = deserializer.Deserialize(data);
                var pageInfo = owner.PageInfo(json);

                Result = owner.ResultBuilder(json);

                foreach (var i in (IList)Result)
                {
                    finalResult.Add(i);
                }

                if ((bool)pageInfo["hasNextPage"] == true)
                {
                    variables["__after"] = (string)pageInfo["endCursor"];
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
