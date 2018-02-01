using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Octokit.GraphQL.Core
{
    public class PagedList<T> : IPagedList<T>
        where T : IQueryableList
    {
        public PagedList(
            Expression expression,
            MethodInfo method,
            LambdaExpression selector)
        {
            Expression = expression;
            Method = method;
            Selector = selector;
        }

        public Expression Expression { get; }
        public MethodInfo Method { get; }
        public LambdaExpression Selector { get; }

        public static PagedList<T> Create<TObject>(
            Expression expression,
            Expression<Func<TObject, T>> method)
        {
            var methodCall = (MethodCallExpression)method.Body;
            return new PagedList<T>(expression, methodCall.Method, null);
        }
    }
}
