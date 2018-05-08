using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Deserializers;

namespace Octokit.GraphQL.Core.UnitTests
{
    static class QueryExtensions
    {
        public static Expression GetExpression<T>(this ICompiledQuery<T> query)
        {
            return ((CompiledQuery<T>)query).Expression;
        }

        public static CompiledQuery<T> GetMasterQuery<T>(this ICompiledQuery<T> query)
        {
            return ((PagedQuery<T>)query).MasterQuery;
        }

        public static IReadOnlyList<Subquery> GetSubqueries<T>(this ICompiledQuery<T> query)
        {
            return ((PagedQuery<T>)query).Subqueries;
        }

        public static T Deserialize<T>(this ICompiledQuery<T> query, string data)
        {
            var q = (CompiledQuery<T>)query;
            return new ResponseDeserializer().Deserialize(q, data);
        }
    }
}
