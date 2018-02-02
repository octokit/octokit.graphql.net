using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit.GraphQL
{
    public interface IQuery<TResult>
    {
        Task<TResult> Run(IConnection connection, IDictionary<string, object> variables);
    }
}