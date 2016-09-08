using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace LinqToGraphQL
{
    public class FieldQuery<T> : IQueryable<T>
    {
        public FieldQuery(IQueryProvider provider, Expression expression)
        {
            Expression = expression;
            Provider = provider;
        }

        public Type ElementType => typeof(T);
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }

        public IEnumerator<T> GetEnumerator()
        {
            return Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Provider.Execute<IEnumerable>(Expression).GetEnumerator();
        }
    }
}
