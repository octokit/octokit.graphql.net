using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL.Core.Utilities
{
    public static class ExpressionMethods
    {
        public static readonly MethodInfo JTokenIndexer = typeof(JToken).GetMethod("get_Item");
        public static readonly MethodInfo JTokenToObject = typeof(JToken).GetMethod(nameof(JToken.ToObject), new Type[0]);
        public static readonly MethodInfo SelectMethod = typeof(ExpressionMethods).GetMethod(nameof(Select));
        public static readonly MethodInfo SelectEntityMethod = typeof(ExpressionMethods).GetMethod(nameof(SelectEntity));
        public static readonly MethodInfo ChildrenOfTypeMethod = typeof(ExpressionMethods).GetMethod(nameof(ChildrenOfType));

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
            else if (token.Type == JTokenType.Null)
            {
                return Enumerable.Empty<T>().AsQueryable();
            }
            else
            {
                return Enumerable.Repeat(selector(token), 1).AsQueryable();
            }
        }

        public static IQueryable<T> SelectMany<T>(JToken token, Func<JToken, IQueryable<T>> selector)
        {
            return selector(token);
        }

        public static IQueryable<JToken> ChildrenOfType(IEnumerable<JToken> parentToken, string typeName)
        {
            return parentToken.Where(x => (string)x["__typename"] == typeName).AsQueryable();
        }
    }
}
