using System;
using System.Collections;

namespace Octokit.GraphQL.Core
{
    /// <summary>
    /// Represents an <see cref="IQueryRunner"/> for an <see cref="ISubquery"/>.
    /// </summary>
    public interface ISubqueryRunner : IQueryRunner
    {
        /// <summary>
        /// Called to tell the runner where a specified subquery should store its results.
        /// </summary>
        /// <param name="query">The subquery.</param>
        /// <param name="result">The target collection.</param>
        void SetQueryResultSink(ISubquery query, IList result);
    }
}
