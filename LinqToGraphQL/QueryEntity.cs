using System;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToGraphQL
{
    public class QueryEntity : IQueryEntity
    {
        protected QueryEntity(IQueryProvider provider)
        {
            Provider = provider;
            Expression = Expression.Constant(this);
        }

        protected QueryEntity(IQueryProvider provider, Expression expression)
        {
            Provider = provider;
            Expression = expression;
        }

        public Expression Expression { get; }
        public IQueryProvider Provider { get; }
    }
}
