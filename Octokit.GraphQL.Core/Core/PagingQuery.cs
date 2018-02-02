using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core
{
    public class PagingQuery<TResult> : IQuery<IEnumerable<TResult>>
    {
        CompiledQuery<Page<TResult>> compiled;

        public PagingQuery(Expression expression)
        {
            compiled = new QueryBuilder().Build<Page<TResult>>(expression);
        }

        public async Task<IEnumerable<TResult>> Run(IConnection connection, IDictionary<string, object> variables)
        {
            var vars = new Dictionary<string, object>
            {
                { "first", 100 },
                { "after", null },
            };

            var result = new List<TResult>();

            while (true)
            {
                var page = await connection.Run(compiled, vars);

                result.AddRange(page.Items);

                if (page.HasNextPage)
                {
                    vars["after"] = page.EndCursor;
                }
                else
                {
                    break;
                }
            }

            return result;
        }
    }
}
