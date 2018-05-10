using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Deserializers;
using Octokit.GraphQL.Core.Syntax;

namespace Octokit.GraphQL.Core
{
    public class SimpleSubquery<TResult> : SimpleQuery<TResult>, ISubquery
    {
        public SimpleSubquery(
            OperationDefinition operationDefinition,
            Expression<Func<JObject, TResult>> expression,
            Expression<Func<JObject, JToken>> parentId,
            Expression<Func<JObject, JToken>> pageInfo,
            Expression<Func<JObject, JToken>> parentPageInfo)
            : base(operationDefinition, expression)
        {
            ParentId = parentId.Compile();
            PageInfo = pageInfo.Compile();
            ParentPageInfo = parentPageInfo.Compile();
        }

        public Func<JObject, JToken> ParentId { get; }
        public Func<JObject, JToken> PageInfo { get; }
        public Func<JObject, JToken> ParentPageInfo { get; }

        public IQueryRunner Start(
            IConnection connection,
            string id,
            string after,
            IDictionary<string, object> variables)
        {
            return new Runner(this, connection, id, after, variables);
        }

        public static ISubquery Create(
            Type resultType,
            OperationDefinition operationDefinition,
            Expression expression,
            Expression<Func<JObject, JToken>> parentId,
            Expression<Func<JObject, JToken>> pageInfo,
            Expression<Func<JObject, JToken>> parentPageInfo)
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
                parentId,
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

            public Runner(
               SimpleSubquery<TResult> owner,
               IConnection connection,
               string id,
               string after,
               IDictionary<string, object> variables)
            {
                this.owner = owner;
                this.connection = connection;
                this.variables = variables?.ToDictionary(x => x.Key, x => x.Value) ?? 
                    new Dictionary<string, object>();
                this.variables["__id"] = id;
                this.variables["__after"] = after;
            }

            public TResult Result { get; private set; }

            object IQueryRunner.Result => Result;

            public async Task<bool> RunPage()
            {
                var payload = owner.GetPayload(variables);
                var data = await connection.Run(payload);
                var json = deserializer.Deserialize(data);
                var pageInfo = owner.PageInfo(json);

                Result = owner.CompiledExpression(json);

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
