using System;
using System.Threading.Tasks;

namespace Octokit.GraphQL.Core
{
    public interface IQueryRunner<TResult>
    {
        TResult Result { get; }
        Task<bool> RunPage();
    }
}
