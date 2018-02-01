using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Octokit.GraphQL.Core.Builders
{
    public class PagedQueryBuilder
    {
        public PagingQuery<IEnumerable<T>> Build<T>(IPagedList<T> pages)
            where T : IPagingConnection
        {
            throw new NotImplementedException();
        }

        public Expression BuildPageExpression<T>(IPagedList<T> pages)
            where T : IPagingConnection
        {
            var connectionType = pages.Expression.Type;
            var resultType = pages.Selector.ReturnType;
            var pageModelType = typeof(Page<>).MakeGenericType(resultType);

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
                    QueryableListExtensions.SelectMethod.MakeGenericMethod(nodeType, pages.Selector.ReturnType),
                    Expression.Property(connectionParameter, "Nodes"),
                    pages.Selector));

            // Builds the select lambda:
            //
            // connection => new PageModel<TResult>
            // {
            //     HasNextPage = connection.PageInfo.HasNextPage,
            //     EndCursor = connection.PageInfo.EndCursor,
            //     Items = connection.Nodes.Select(pages.Selector).ToList(),
            // }
            var connectionPageInfo = Expression.Property(connectionParameter, "PageInfo");
            var selectPageModel = Expression.Lambda(
                Expression.MemberInit(
                    Expression.New(pageModelType),
                    Expression.Bind(
                        pageModelType.GetRuntimeProperty("HasNextPage"),
                        Expression.Property(connectionPageInfo, "HasNextPage")),
                    Expression.Bind(
                        pageModelType.GetRuntimeProperty("EndCursor"),
                        Expression.Property(connectionPageInfo, "EndCursor")),
                    Expression.Bind(
                        pageModelType.GetRuntimeProperty("Items"),
                        selectNodes)),
                connectionParameter);

            // Builds the final expression:
            //
            // pages.Expression.Select(connection => new PageModel<TResult>
            // {
            //     HasNextPage = connection.PageInfo.HasNextPage,
            //     EndCursor = connection.PageInfo.EndCursor,
            //     Items = connection.Nodes.Select(pages.Selector).ToList(),
            // }
            return Expression.Call(
                QueryableValueExtensions.SelectMethod.MakeGenericMethod(connectionType, pageModelType),
                pages.Expression,
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
