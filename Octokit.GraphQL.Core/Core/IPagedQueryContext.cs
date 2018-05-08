using System;
using System.Collections;

namespace Octokit.GraphQL.Core
{
    public interface IPagedQueryContext
    {
        void SetQueryResultSink(Subquery query, IList result);
    }
}
