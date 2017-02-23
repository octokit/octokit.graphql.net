using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace LinqToGraphQL.Deserializers
{
    public class ResponseDeserializer
    {
        public IEnumerable<TResult> Deserialize<TResult>(Query<TResult> query, string data)
        {
            return Deserialize(query, JObject.Parse(data));
        }

        public IEnumerable<TResult> Deserialize<TResult>(Query<TResult> query, JObject data)
        {
            return query.CompiledExpression(data);
        }
    }
}
