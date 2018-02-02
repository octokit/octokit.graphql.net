using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL
{
    public static class ConnectionExtensions
    {
        public static Task<T> Run<T>(
            this IConnection connection,
            IQuery<T> query,
            IDictionary<string, object> variables = null)
        {
            return query.Run(connection, variables);
        }

        public static Task<T> Run<T>(
            this IConnection connection,
            IQueryableValue<T> expression)
        {
            return connection.Run(new QueryBuilder().Build(expression));
        }

        public static Task<IEnumerable<T>> Run<T>(
            this IConnection connection,
            IQueryableList<T> expression)
        {
            return connection.Run(new QueryBuilder().Build(expression));
        }
    }
}
