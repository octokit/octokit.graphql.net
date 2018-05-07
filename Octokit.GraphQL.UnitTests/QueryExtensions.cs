using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Deserializers;

namespace Octokit.GraphQL.UnitTests
{
    static class QueryExtensions
    {
        public static Expression GetExpression<T>(this ICompiledQuery<T> query)
        {
            return ((CompiledQuery<T>)query).Expression;
        }

        public static T Deserialize<T>(this ICompiledQuery<T> query, string data)
        {
            var q = (CompiledQuery<T>)query;
            return new ResponseDeserializer().Deserialize(q, data);
        }
    }
}
