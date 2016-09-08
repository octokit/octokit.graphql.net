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

        protected Argument<T> Arg<T>(T value)
        {
            return new Argument<T>(value);
        }

        protected IQueryable<T> MethodCall<T>(string method, params Argument[] args)
        {
            var argTypes = args.Select(x => x.Type).ToArray();
            var argExpressions = args.Select(x => Expression.Constant(x.Value, x.Type));
            var methodInfo = GetType().GetRuntimeMethod(method, argTypes);
            var expression = Expression.Call(Expression.Constant(this), methodInfo, argExpressions);

            return new FieldQuery<T>(_provider, expression);
        }

        protected abstract class Argument
        {
            public Type Type { get; protected set; }
            public object Value { get; protected set; }
        }

        protected class Argument<T> : Argument
        {
            public Argument(T value)
            {
                Type = typeof(T);
                Value = value;
            }
        }
    }
}
