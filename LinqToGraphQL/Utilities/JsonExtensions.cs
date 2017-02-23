using System;
using Newtonsoft.Json.Linq;

namespace LinqToGraphQL.Utilities
{
    public static class JsonExtensions
    {
        public static T JsonSelect<T>(this JToken token, Func<JToken, T> selector)
        {
            return selector(token);
        }
    }
}
