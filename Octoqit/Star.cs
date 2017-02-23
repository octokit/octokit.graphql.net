using System;
using System.Linq;
using LinqToGraphQL;

namespace Octoqit
{
    public class Star : QueryEntity
    {
        public Star(IQueryProvider provider)
            : base(provider)
        {
        }

        public string Id { get; }
        public DateTime CreatedAt { get; }
    }
}
