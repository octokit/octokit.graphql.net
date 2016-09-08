using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToGraphQL
{
    public class QueryEntity
    {
        IQueryProvider _provider;

        protected QueryEntity(IQueryProvider provider)
        {
            _provider = provider;
        }

        protected IQueryable<T> MethodCall<T>(string method, params object[] args)
        {
            var argTypes = args.Select(x => x.GetType()).ToArray();
            var argExpressions = args.Select(x => Expression.Constant(x));
            var methodInfo = GetType().GetRuntimeMethod(method, argTypes);
            var expression = Expression.Call(Expression.Constant(this), methodInfo, argExpressions);

            return new FieldQuery<T>(_provider, expression);
        }
    }
}
