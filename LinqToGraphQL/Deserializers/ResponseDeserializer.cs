using System;
using Newtonsoft.Json.Linq;

namespace LinqToGraphQL.Deserializers
{
    public class ResponseDeserializer
    {
        public TResult Deserialize<TResult>(Query<TResult> query, string data)
        {
            return Deserialize(query, JObject.Parse(data));
        }

        public TResult Deserialize<TResult>(Query<TResult> query, JObject data)
        {
            return query.CompiledExpression(data);
        }
    }
}
