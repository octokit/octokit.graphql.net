using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.GraphQL.Core
{
    public class PagingQuery<TResult> : IQuery<TResult>
    {
        public Task<TResult> Run(IConnection connection, IDictionary<string, object> variables)
        {
            throw new NotImplementedException();
        }
    }
}
