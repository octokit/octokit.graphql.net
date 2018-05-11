using System;
using System.Threading.Tasks;

namespace Octokit.GraphQL.Core
{
    /// <summary>
    /// Runns a potentially paged query.
    /// </summary>
    public interface IQueryRunner
    {
        /// <summary>
        /// Gets the result of the query.
        /// </summary>
        object Result { get; }

        /// <summary>
        /// Runs the next page of the query.
        /// </summary>
        /// <returns></returns>
        Task<bool> RunPage();
    }

    /// <summary>
    /// Runns a potentially paged query.
    /// </summary>
    public interface IQueryRunner<out TResult> : IQueryRunner
    {
        /// <summary>
        /// Gets the result of the query.
        /// </summary>
        new TResult Result { get; }
    }
}
