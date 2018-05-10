using System;
using System.Collections;

namespace Octokit.GraphQL.Core
{
    public interface ISubqueryRunner : IQueryRunner
    {
        void SetQueryResultSink(ISubquery query, IList result);
    }
}
