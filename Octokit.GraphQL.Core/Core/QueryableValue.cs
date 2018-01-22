using System;
using System.Linq;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public class QueryableValue : IQueryableValue
    {
        private IQueryProvider provider;
        private Expression expression;

        protected QueryableValue(IQueryProvider provider)
        {
            this.provider = provider;
            expression = Expression.Constant(this);
        }

        protected QueryableValue(IQueryProvider provider, Expression expression)
        {
            this.provider = provider;
            this.expression = expression;
        }

        public Expression Expression => expression;
        public IQueryProvider Provider => provider;
    }

    public class QueryableValue<T> : QueryableValue, IQueryableValue<T>
    {
        public QueryableValue(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        {
        }
    }
}
