using System;
using System.Linq;
using System.Linq.Expressions;

namespace Octokit.GraphQL.Core
{
    public class QueryableList<T> : IQueryableList<T>
    {
        public QueryableList(IQueryProvider provider, Expression expression)
        {
            Expression = expression;
            Provider = provider;
        }

        public Type ElementType => typeof(T);
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }
    }
}
