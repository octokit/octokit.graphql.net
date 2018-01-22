using System;
using System.Linq;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public class QueryableValue<T> : IQueryableValue<T>
    {
        private IQueryProvider provider;
        private Expression expression;

        public QueryableValue(IQueryProvider provider)
        {
            this.provider = provider;
            expression = Expression.Constant(this);
        }

        public QueryableValue(IQueryProvider provider, Expression expression)
        {
            this.provider = provider;
            this.expression = expression;
        }

        public Expression Expression => expression;
        public IQueryProvider Provider => provider;
    }
}
