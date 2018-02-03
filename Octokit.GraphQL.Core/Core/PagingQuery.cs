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
        public PagingQuery(Expression expression)
        {
            Master = new QueryBuilder().Build<Page<TResult>>(expression);
        }

        public CompiledQuery<Page<TResult>> Master;

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
                var page = await connection.Run(Master, vars);

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
