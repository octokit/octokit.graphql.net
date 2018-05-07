using System;
using System.Threading.Tasks;

namespace Octokit.GraphQL.Core
{
    public interface IQueryRunner
    {
        Task<bool> RunPage();
    }

    public interface IQueryRunner<out TResult> : IQueryRunner
    {
        TResult Result { get; }
    }
}
