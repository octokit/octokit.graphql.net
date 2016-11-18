using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace LinqToGraphQL
{
    public class QueryEntity
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

        protected MethodArg<T> Arg<T>(T value)
        {
            return new MethodArg<T>(value);
        }

        protected IQueryable<T> MethodCall<T>(string method, params MethodArg[] args)
        {
            var expression = MethodCallExpression(method, args);
            return new FieldQuery<T>(Provider, expression);
        }

        protected Expression MethodCallExpression(string method, params MethodArg[] args)
        {
            var argTypes = args.Select(x => x.Type).ToArray();
            var argExpressions = args.Select(x => Expression.Constant(x.Value, x.Type));
            var methodInfo = GetType().GetRuntimeMethod(method, argTypes);
            return Expression.Call(Expression.Constant(this), methodInfo, argExpressions);
        }

        protected IQueryable<T> CollectionProperty<T>(string name)
        {
            var property = GetType().GetProperty(name);
            var expression = Expression.Property(Expression.Constant(this), property);
            return new FieldQuery<T>(Provider, expression);
        }

        protected abstract class MethodArg
        {
            public Type Type { get; protected set; }
            public object Value { get; protected set; }
        }

        protected class MethodArg<T> : MethodArg
        {
            public MethodArg(T value)
            {
                Type = typeof(T);
                Value = value;
            }
        }
    }
}
