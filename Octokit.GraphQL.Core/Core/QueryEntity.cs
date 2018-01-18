using System;
using System.Linq;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public class QueryEntity : IQueryEntity
    {
        private IQueryProvider provider;
        private Expression expression;

        protected QueryEntity(IQueryProvider provider)
        {
            this.provider = provider;
            expression = Expression.Constant(this);
        }

        protected QueryEntity(IQueryProvider provider, Expression expression)
        {
            this.provider = provider;
            this.expression = expression;
        }

        public Expression Expression => expression;
        public IQueryProvider Provider => provider;
    }
}
