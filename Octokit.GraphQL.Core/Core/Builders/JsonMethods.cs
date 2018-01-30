using System;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL.Core.Builders
{
    internal static class JsonMethods
    {
        public static readonly MethodInfo JTokenIndexer = typeof(JToken).GetTypeInfo().GetDeclaredMethod("get_Item");
        public static readonly MethodInfo JTokenToObject = GetMethod(typeof(JToken), nameof(JToken.ToObject), new Type[0]);

        static MethodInfo GetMethod(Type type, string name, params Type[] parameters)
        {
            return type.GetTypeInfo().GetDeclaredMethods(name)
                .First(x => Enumerable.Select(x.GetParameters(), y => y.ParameterType).SequenceEqual(parameters));
        }
    }
}
