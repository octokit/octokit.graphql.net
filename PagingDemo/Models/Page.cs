using System;
using System.Collections.Generic;

namespace PagingDemo.Models
{
    class Page<T> : List<T>
    {
        public bool HasNextPage { get; set; }
        public string EndCursor { get; set; }
        public List<T> Items
        {
            get { return this; }
            set { AddRange(value); }
        }
    }
}
