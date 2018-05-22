using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Internal;

namespace Octokit.GraphQL
{
    public static class PagingConnectionExtensions
    {
        public static readonly MethodInfo AllPagesMethod = GetMethodInfo(nameof(AllPages));

        [MethodId(nameof(AllPages))]
        public static IQueryableList<TResult> AllPages<TResult>(this IPagingConnection<TResult> source)
            where TResult : IQueryableValue
        {
            return new QueryableList<TResult>(
                Expression.Call(
                    null,
                    GetMethodInfoOf(() => AllPages<TResult>(default)),
                    new Expression[] { source.Expression }));
        }

        private static MethodInfo GetMethodInfo(string id)
        {
            return typeof(PagingConnectionExtensions)
                .GetTypeInfo()
                .DeclaredMethods
                .Where(x => x.GetCustomAttribute<MethodIdAttribute>()?.Id == id)
                .SingleOrDefault();
        }

        private static MethodInfo GetMethodInfoOf<T>(Expression<Func<T>> expression)
        {
            var body = (MethodCallExpression)expression.Body;
            return body.Method;
        }
    }
}
