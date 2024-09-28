using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.Deserializers;

namespace Octokit.GraphQL.Core.UnitTests
{
    static class QueryExtensions
    {
        public static Expression GetResultBuilderExpression<T>(this ICompiledQuery<T> query)
        {
            return ExpressionCompiler.GetSourceExpression(((SimpleQuery<T>)query).ResultBuilder);
        }

        public static SimpleQuery<T> GetMasterQuery<T>(this ICompiledQuery<T> query)
        {
            return ((PagedQuery<T>)query).MasterQuery;
        }

        public static IReadOnlyList<ISubquery> GetSubqueries<T>(this ICompiledQuery<T> query)
        {
            return ((PagedQuery<T>)query).Subqueries;
        }

        public static T Deserialize<T>(this ICompiledQuery<T> query, string data)
        {
            var q = (SimpleQuery<T>)query;
            return new ResponseDeserializer().Deserialize(q, data);
        }
    }
}
