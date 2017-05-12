using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL.Core.Deserializers
{
    public class ResponseDeserializer
    {
        public IEnumerable<TResult> Deserialize<TResult>(GraphQLQuery<TResult> query, string data)
        {
            return Deserialize(query, JObject.Parse(data));
        }

        public IEnumerable<TResult> Deserialize<TResult>(GraphQLQuery<TResult> query, JObject data)
        {
            if (data["errors"] != null)
            {
                throw DeserializeExceptions((JArray)data["errors"]);
            }

            return query.CompiledExpression(data);
        }

        private Exception DeserializeExceptions(JArray errors)
        {
            if (errors.Count == 1)
            {
                return DeserializeException(errors[0]);
            }
            else
            {
                var inner = Enumerable.Select(errors, x => DeserializeException(x)).ToList();
                return new AggregateException(inner);
            }
        }

        private Exception DeserializeException(JToken error)
        {
            return new GraphQLQueryException(
                (string)error["message"],
                (int)error["locations"][0]["line"],
                (int)error["locations"][0]["column"]);
        }
    }
}
