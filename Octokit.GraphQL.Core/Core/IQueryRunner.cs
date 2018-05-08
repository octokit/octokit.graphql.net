using System;
using System.Threading.Tasks;

namespace Octokit.GraphQL.Core
{
    public interface IQueryRunner
    {
        object Result { get; }
        Task<bool> RunPage();
    }

    public interface IQueryRunner<out TResult> : IQueryRunner
    {
        new TResult Result { get; }
    }
}
