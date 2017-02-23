using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace LinqToGraphQL.Utilities
{
    public static class JsonUtilities
    {
        public static IEnumerable<T> Select<T>(JToken token, Func<JToken, T> selector)
        {
            if (token.Type == JTokenType.Array)
            {
                return token.Select(selector);
            }
            else
            {
                return Enumerable.Repeat(selector(token), 1);
            }
        }
    }
}
