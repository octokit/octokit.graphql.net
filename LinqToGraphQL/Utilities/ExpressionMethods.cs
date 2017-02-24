using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace LinqToGraphQL.Utilities
{
    public static class ExpressionMethods
    {
        public static readonly MethodInfo JTokenIndexer = typeof(JToken).GetMethod("get_Item");
        public static readonly MethodInfo JTokenToObject = typeof(JToken).GetMethod(nameof(JToken.ToObject), new Type[0]);
        public static readonly MethodInfo SelectMethod = typeof(ExpressionMethods).GetMethod(nameof(Select));
        public static readonly MethodInfo SelectEntityMethod = typeof(ExpressionMethods).GetMethod(nameof(SelectEntity));

        public static IQueryable<T> Select<T>(IQueryable<JToken> tokens, Func<JToken, T> selector)
        {
            return tokens.Select(selector).AsQueryable();
        }

        public static IQueryable<T> SelectEntity<T>(JToken token, Func<JToken, T> selector)
        {
            if (token.Type == JTokenType.Array)
            {
                return token.Select(selector).AsQueryable();
            }
            else
            {
                return Enumerable.Repeat(selector(token), 1).AsQueryable();
            }
        }
    }
}
