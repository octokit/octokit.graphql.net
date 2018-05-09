using System;
using System.Collections;

namespace Octokit.GraphQL.Core
{
    public interface IPagedQueryContext
    {
        void SetQueryResultSink(ISubquery query, IList result);
    }
}
