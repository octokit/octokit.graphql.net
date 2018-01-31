using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Internal;

namespace Octokit.GraphQL
{
    public static class QueryableInterfaceExtensions
    {
        public static readonly MethodInfo CastMethod = GetMethodInfo(nameof(CastMethod));

        [MethodId(nameof(CastMethod))]
        public static IQueryableValue<TResult> Cast<TResult>(this IQueryableInterface source)
            where TResult : IQueryableValue
        {
            return new QueryableValue<TResult>(
                Expression.Call(
                    null,
                    GetMethodInfoOf(() => Cast<TResult>(default(IQueryableInterface))),
                    new Expression[] { source.Expression }));
        }

        private static MethodInfo GetMethodInfo(string id)
        {
            return typeof(QueryableInterfaceExtensions)
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
