using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL.Core.Deserializers
{
    public class ResponseDeserializer
    {
        public JObject Deserialize(string data)
        {
            var result = JObject.Parse(data);

            if (result["errors"] != null)
            {
                throw DeserializeExceptions((JArray)result["errors"]);
            }

            return result;
        }

        public TResult Deserialize<TResult>(SimpleQuery<TResult> query, string data)
        {
            return Deserialize(query, JObject.Parse(data));
        }

        public TResult Deserialize<TResult>(SimpleQuery<TResult> query, JObject data)
        {
            if (data["errors"] != null)
            {
                throw DeserializeExceptions((JArray)data["errors"]);
            }

            return query.ResultBuilder(data);
        }

        public TResult Deserialize<TResult>(Func<JObject, TResult> deserialize, string data)
        {
            return Deserialize(deserialize, JObject.Parse(data));
        }

        public TResult Deserialize<TResult>(Func<JObject, TResult> deserialize, JObject data)
        {
            if (data["errors"] != null)
            {
                throw DeserializeExceptions((JArray)data["errors"]);
            }

            return deserialize(data);
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
            var message = (string)error["message"];
            var location = (error["locations"] as JArray)?.FirstOrDefault();
            var line = (int?)location?["line"] ?? 0;
            var column = (int?)location?["column"] ?? 0;

            if (IsRateLimitError(message))
            {
                return new RateLimitExceededException(message, line, column);
            }

            return new ResponseDeserializerException(
                message,
                line,
                column);
        }

        private static bool IsRateLimitError(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return false;
            }

            return message.IndexOf("rate limit", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
