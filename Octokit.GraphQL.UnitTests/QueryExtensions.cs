using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;
using Octokit.GraphQL.Core.Deserializers;

namespace Octokit.GraphQL.UnitTests
{
    static class QueryExtensions
    {
        public static Expression GetResultBuilderExpression<T>(this ICompiledQuery<T> query)
        {
            return ExpressionCompiler.GetSourceExpression(((SimpleQuery<T>)query).ResultBuilder);
        }

        public static T Deserialize<T>(this ICompiledQuery<T> query, string data)
        {
            var q = (SimpleQuery<T>)query;
            return new ResponseDeserializer().Deserialize(q, data);
        }
    }
}
