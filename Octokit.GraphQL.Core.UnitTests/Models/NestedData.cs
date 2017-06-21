using System;
using System.Linq;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class NestedData : QueryEntity
    {
        public NestedData(IQueryProvider provider)
            : base(provider)
        {
        }

        public NestedData(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public IQueryable<Simple> Items => this.CreateProperty(x => x.Items);

        internal NestedData Create(IQueryProvider provider, Expression expression)
        {
            return new NestedData(provider, expression);
        }
    }
}
