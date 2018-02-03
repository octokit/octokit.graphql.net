using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static Octokit.GraphQL.Variable;

namespace Octokit.GraphQL.Core.Builders
{
    public class PagedQueryBuilder
    {
        public PagingQuery<T> Build<T>(IPagedList<IPagingConnection<T>> paging)
        {
            var rewritten = RewriteExpression(paging.Expression);
            return new PagingQuery<T>(rewritten);
        }

        /// <summary>
        /// Rewrites an auto-paging query expression to its underlying paged implementation.
        /// </summary>
        /// <typeparam name="T">The result type.</typeparam>
        /// <param name="paging">The paging expression.</param>
        /// <returns>The rewritten expression.</returns>
        /// <remarks>
        /// An auto-paging expression must be rewritten into a query with variables describing
        /// the page to read. For example:
        /// 
        /// ```
        /// var query = new Query()
        ///    .Repository("foo", "bar")
        ///    .PullRequests() // Auto-paging query
        ///    .Select(x => x.Title);
        /// ```
        /// 
        /// Will be rewritten as:
        /// 
        /// ```
        /// var query = new TestQuery()
        ///    .Repository("foo", "bar")
        ///    .PullRequests(Var("first"), Var("after"), Var("last"), Var("before"))
        ///    .Select(connection => new Page<string>
        ///    {
        ///        HasNextPage = connection.PageInfo.HasNextPage,
        ///        EndCursor = connection.PageInfo.EndCursor,
        ///        Items = connection.Nodes.Select(x => x.Title).ToList(),
        ///    });
        /// ```
        /// </remarks>
        public MethodCallExpression RewriteExpression(Expression expression)
        {
            var selectMethod = GetSelectMethod(expression);
            var sourceConnectionType = GetPagingConnectionType(selectMethod.Arguments[0]);
            var sourceNodeType = GetPagingConnectionNodeType(sourceConnectionType);
            var resultConnectionType = GetPagingConnectionType(expression);
            var resultNodeType = GetPagingConnectionNodeType(resultConnectionType);
            var pageType = typeof(Page<>).MakeGenericType(resultNodeType);

            // The `connection` parameter.
            var connectionParameter = Expression.Parameter(sourceConnectionType, "connection");

            //// Builds `connection.Nodes`.
            var connectionNodes = Expression.Property(connectionParameter, "Nodes");

            //// Builds `connection.Nodes.Select(pages.Selector).ToList`.
            var selectNodes = Expression.Call(
                QueryableListExtensions.ToListMethod.MakeGenericMethod(resultNodeType),
                Expression.Call(
                    QueryableListExtensions.SelectMethod.MakeGenericMethod(sourceNodeType, resultNodeType),
                    Expression.Property(connectionParameter, "Nodes"),
                    selectMethod.Arguments[1]));

            // Builds the select lambda:
            //
            // connection => new Page<TResult>
            // {
            //     HasNextPage = connection.PageInfo.HasNextPage,
            //     EndCursor = connection.PageInfo.EndCursor,
            //     Items = connection.Nodes.Select(pages.Selector).ToList(),
            // }
            var connectionPageInfo = Expression.Property(connectionParameter, "PageInfo");
            var selectPage = Expression.Lambda(
                Expression.MemberInit(
                    Expression.New(pageType),
                    Expression.Bind(
                        pageType.GetRuntimeProperty("HasNextPage"),
                        Expression.Property(connectionPageInfo, "HasNextPage")),
                    Expression.Bind(
                        pageType.GetRuntimeProperty("EndCursor"),
                        Expression.Property(connectionPageInfo, "EndCursor")),
                    Expression.Bind(
                        pageType.GetRuntimeProperty("Items"),
                        selectNodes)),
                connectionParameter);

            // Gets the target query, i.e. the related query with the first, after, last, before args.
            var target = BuildTarget(selectMethod.Arguments[0]);

            //// Builds the final expression:
            ////
            //// targetExpression.Select(connection => new Page<TResult>
            //// {
            ////     HasNextPage = connection.PageInfo.HasNextPage,
            ////     EndCursor = connection.PageInfo.EndCursor,
            ////     Items = connection.Nodes.Select(pages.Selector).ToList(),
            //// }
            return Expression.Call(
                QueryableValueExtensions.SelectMethod.MakeGenericMethod(sourceConnectionType, pageType),
                target,
                selectPage);
            throw new NotImplementedException();
        }

        private static Expression BuildTarget(Expression source)
        {
            if (source is MethodCallExpression m &&
                typeof(IQueryableValue).GetTypeInfo().IsAssignableFrom(m.Method.DeclaringType.GetTypeInfo()))
            {
                var pagingParameters = new[]
                {
                    typeof(Arg<int>?),
                    typeof(Arg<string>?),
                    typeof(Arg<int>?),
                    typeof(Arg<string>?),
                };

                var pagingArgs = new []
                {
                    Expression.Constant(new Arg<int>("first", 100), typeof(Arg<int>?)),
                    Expression.Constant(new Arg<string>("after", null), typeof(Arg<string>?)),
                    Expression.Constant(new Arg<int>("last", 100), typeof(Arg<int>?)),
                    Expression.Constant(new Arg<string>("before", null), typeof(Arg<string>?)),
                };

                var methodName = m.Method.Name + "Internal";
                var parameters = pagingParameters.Concat(m.Arguments.Select(x => x.Type)).ToArray();
                var targetMethod = m.Method.DeclaringType.GetRuntimeMethods()
                    .FirstOrDefault(x => 
                        x.Name == methodName &&
                        x.GetParameters().Select(y => y.ParameterType).SequenceEqual(parameters));

                if (targetMethod == null)
                {
                    throw new NotSupportedException(
                        $"Could not find method {m.Method.DeclaringType.Name}.{methodName}.");
                }

                var args = pagingArgs.Concat(m.Arguments).ToList();
                return Expression.Call(
                    m.Object,
                    targetMethod,
                    args);
            }
            else
            {
                throw new NotSupportedException(
                    $"Expected a method call on an IQueryableValue but got '{source}'");
            }
        }

        private static MethodCallExpression GetSelectMethod(Expression expression)
        {
            if (expression is MethodCallExpression m &&
                m.Method.GetGenericMethodDefinition() == PagedListExtensions.SelectMethod)
            {
                return m;
            }
            else
            {
                throw new NotSupportedException(
                    $"Expected an IPagedList<T>.Select() expression but got '{expression}'.");
            }
        }

        private static Type GetPagingConnectionType(Expression expression)
        {
            var t = expression.Type;

            if (t.IsConstructedGenericType && t.GetGenericTypeDefinition() == typeof(IPagedList<>))
            {
                return t.GenericTypeArguments[0];
            }
            else
            {
                throw new NotSupportedException($"Expected an 'IPagedList<>' but got {t}.");
            }
        }

        private static Type GetPagingConnectionNodeType(Type type)
        {
            bool IsPagingConnection(Type t)
            {
                return t.IsConstructedGenericType &&
                    t.GetGenericTypeDefinition() == typeof(IPagingConnection<>);
            }

            if (IsPagingConnection(type))
            {
                return type.GenericTypeArguments[0];
            }

            var i = type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(IsPagingConnection);

            if (i != null)
            {
                return i.GenericTypeArguments[0];
            }
            else
            {
                throw new NotSupportedException($"Expected an 'IPagingConnection<T>' but got {type}.");
            }
        }

        private static IEnumerable<Expression> BuildPagingVariables(MethodInfo method)
        {
            foreach (var p in method.GetParameters())
            {
                if (p.Name == "first" && p.ParameterType == typeof(Arg<int>?))
                {
                    yield return Expression.Constant(new Arg<int>("first", 0), typeof(Arg<int>?));
                }
                else if (p.Name == "after" && p.ParameterType == typeof(Arg<string>?))
                {
                    yield return Expression.Constant(new Arg<string>("after", null), typeof(Arg<string>?));
                }
                else
                {
                    yield return Expression.Constant(null, p.ParameterType);
                }
            }
        }
    }
}
