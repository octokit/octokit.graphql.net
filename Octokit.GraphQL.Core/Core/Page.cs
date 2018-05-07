using System;
using System.Collections.Generic;

namespace Octokit.GraphQL.Core
{
    public class Page<T> : List<T>
    {
        public Page(
            bool hasNextPage,
            string endCursor,
            IEnumerable<T> items)
        {
            HasNextPage = hasNextPage;
            EndCursor = endCursor;
        }

        public bool HasNextPage { get; }
        public string EndCursor { get; }
    }
}
