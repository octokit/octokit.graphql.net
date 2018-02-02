using System;
using System.Collections;
using System.Collections.Generic;

namespace Octokit.GraphQL.Core
{
    public class Page<T> : IEnumerable<T>
    {
        public bool HasNextPage { get; set; }
        public string EndCursor { get; set; }
        public IList<T> Items { get; set; }

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Items.GetEnumerator();
    }
}
