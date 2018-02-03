using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Octokit.GraphQL.Core
{
    public class Page<T>
    {
        public bool HasNextPage { get; set; }
        public string EndCursor { get; set; }
        public IList<T> Items { get; set; }
    }
}
