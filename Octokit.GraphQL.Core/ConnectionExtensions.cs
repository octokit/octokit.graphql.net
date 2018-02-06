using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
