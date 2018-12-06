using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Octokit.GraphQL.Core;

namespace Octokit.GraphQL
{
    public static class ConnectionExtensions
    {
        public static Task<T> Run<T>(
            this IConnection connection,
            IQueryableValue<T> expression,
            CancellationToken cancellationToken = default)
        {
            return connection.Run(expression.Compile(), cancellationToken: cancellationToken);
        }

        public static Task<IEnumerable<T>> Run<T>(
            this IConnection connection,
            IQueryableList<T> expression,
            CancellationToken cancellationToken = default)
        {
            return connection.Run(expression.Compile(), cancellationToken: cancellationToken);
        }

        public static async Task<T> Run<T>(
            this IConnection connection,
            ICompiledQuery<T> query,
            Dictionary<string, object> variables = null,
            CancellationToken cancellationToken = default)
        {
            var run = query.Start(connection, variables);
            while (await run.RunPage(cancellationToken).ConfigureAwait(false)) { }
            return run.Result;
        }

        public static async Task<TResult> Run<T1, TResult>(
            this Connection connection,
            Func<Arg<T1>, IQueryableValue<TResult>> queryMethod,
            T1 arg1)
        {
            var method = Compile(queryMethod);
            return await method(connection, arg1);
        }

        static Func<Connection, T1, Task<TResult>> Compile<T1, TResult>(
            Func<Arg<T1>, IQueryableValue<TResult>> queryMethod)
        {
            var arg1 = new Variable("arg1");
            var compiledQuery = CachingCompile(queryMethod, arg1);

            return async (connection, arg) =>
            {
                var vars = new Dictionary<string, object> { { "arg1", arg } };
                return await connection.Run(compiledQuery, vars);
            };
        }

        static ICompiledQuery<TResult> CachingCompile<T1, TResult>(Func<Arg<T1>, IQueryableValue<TResult>> queryMethod, Variable arg1)
        {
            ICompiledQuery<TResult> compiledQuery;
            if (CompiledQueryCache.TryGetValue(queryMethod, out object cachedCompiledQuery))
            {
                compiledQuery = (ICompiledQuery<TResult>)cachedCompiledQuery;
            }
            else
            {
                var query = queryMethod(arg1);
                CompiledQueryCache[queryMethod] = compiledQuery = query.Compile();
            }

            return compiledQuery;
        }

        static IDictionary<Delegate, object> CompiledQueryCache { get; } = new Dictionary<Delegate, object>();
    }
}
