using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    class NestedData : QueryableValue<NestedData>, IQueryableValue<NestedData>
    {
        public NestedData(Expression expression)
            : base(expression)
        {
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public IQueryableList<Simple> NestedItems => this.CreateProperty(x => x.NestedItems);

        internal NestedData Create(Expression expression)
        {
            return new NestedData(expression);
        }
    }
}
