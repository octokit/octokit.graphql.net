using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octokit.GraphQL.Core;

namespace Octokit.GraphQL
{
    public static class ConnectionExtensions
    {
        public static Task<T> Run<T>(
            this IConnection connection,
            IQueryableValue<T> expression)
        {
            return connection.Run(expression.Compile());
        }

        public static Task<IEnumerable<T>> Run<T>(
            this IConnection connection,
            IQueryableList<T> expression)
        {
            return connection.Run(expression.Compile());
        }

        public static async Task<T> Run<T>(
            this IConnection connection,
            ICompiledQuery<T> query,
            Dictionary<string, object> variables = null)
        {
            var run = query.Start(connection, variables);
            while (await run.RunPage()) { }
            return run.Result;
        }
    }
}
