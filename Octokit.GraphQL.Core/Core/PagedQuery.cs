using System;
using System.Collections.Generic;
using System.Text;

namespace Octokit.GraphQL.Core
{
    public class PagedQuery<TResult> : ICompiledQuery<TResult>
    {
        public PagedQuery(
            CompiledQuery<TResult> masterQuery,
            IReadOnlyList<SubQuery> subqueries)
        {
            MasterQuery = masterQuery;
            Subqueries = subqueries;
        }

        public CompiledQuery<TResult> MasterQuery { get; }
        public IReadOnlyList<SubQuery> Subqueries { get; }

        public IQueryRunner<TResult> Start(IConnection connection, Dictionary<string, object> variables)
        {
            throw new NotImplementedException();
        }

        public string ToString(int indentation)
        {
            throw new NotImplementedException();
        }

        IQueryRunner ICompiledQuery.Start(IConnection connection, Dictionary<string, object> variables)
        {
            return Start(connection, variables);
        }
    }
}
