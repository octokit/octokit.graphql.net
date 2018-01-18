using System;
using System.Linq;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class Simple : QueryableValue
    {
        public Simple(IQueryProvider provider)
            : base(provider)
        {
        }

        public Simple(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Number { get; set; }

        internal Simple Create(IQueryProvider provider, Expression expression)
        {
            return new Simple(provider, expression);
        }
    }
}
