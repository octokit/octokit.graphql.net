using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Octokit.GraphQL.Core.Builders
{
    public class PagedQueryBuilder
    {
        public PagingQuery<T> Build<T>(IPagedList<IPagingConnection<T>> paging)
        {
            var rewritten = RewriteExpression(paging);
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
        ///    .PullRequests(first: Var("first"), after: Var("after"))
        ///    .Select(connection => new Page<string>
        ///    {
        ///        HasNextPage = connection.PageInfo.HasNextPage,
        ///        EndCursor = connection.PageInfo.EndCursor,
        ///        Items = connection.Nodes.Select(x => x.Title).ToList(),
        ///    });
        /// ```
        /// </remarks>
        public MethodCallExpression RewriteExpression<T>(IPagedList<T> paging)
            where T : IPagingConnection
        {
            var connectionType = paging.Expression.Type;
            var resultType = paging.Selector.ReturnType;
            var pageType = typeof(Page<>).MakeGenericType(resultType);

            // Gets the `IPagingConnection<TNode>` type.
            var pagingConnectionType = connectionType.GetTypeInfo()
                .ImplementedInterfaces
                .First(x => x.IsConstructedGenericType && 
                            x.GetGenericTypeDefinition() == typeof(IPagingConnection<>));

            // Gets the `TNode` type in `IPagingConnection<TNode>`
            var nodeType = pagingConnectionType.GenericTypeArguments[0];

            // The `connection` parameter passed into the select lambda.
            var connectionParameter = Expression.Parameter(connectionType, "connection");

            // Builds `connection.Nodes`.
            var connectionNodes = Expression.Property(connectionParameter, "Nodes");

            // Builds `connection.Nodes.Select(pages.Selector).ToList`.
            var selectNodes = Expression.Call(
                QueryableListExtensions.ToListMethod.MakeGenericMethod(resultType),
                Expression.Call(
                    QueryableListExtensions.SelectMethod.MakeGenericMethod(nodeType, paging.Selector.ReturnType),
                    Expression.Property(connectionParameter, "Nodes"),
                    paging.Selector));

            // Builds the select lambda:
            //
            // connection => new Page<TResult>
            // {
            //     HasNextPage = connection.PageInfo.HasNextPage,
            //     EndCursor = connection.PageInfo.EndCursor,
            //     Items = connection.Nodes.Select(pages.Selector).ToList(),
            // }
            var connectionPageInfo = Expression.Property(connectionParameter, "PageInfo");
            var selectPageModel = Expression.Lambda(
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

            // Builds the final expression:
            //
            // pages.Expression.Select(connection => new Page<TResult>
            // {
            //     HasNextPage = connection.PageInfo.HasNextPage,
            //     EndCursor = connection.PageInfo.EndCursor,
            //     Items = connection.Nodes.Select(pages.Selector).ToList(),
            // }
            return Expression.Call(
                QueryableValueExtensions.SelectMethod.MakeGenericMethod(connectionType, pageType),
                paging.Expression,
                selectPageModel);
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
