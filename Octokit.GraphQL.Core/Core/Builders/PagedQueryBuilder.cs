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
        public bool InsertVariables { get; set; } = true;
        public int PageSize { get; set; } = 100;

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
            var source = selectMethod.Arguments[0];
            var sourceConnectionType = GetPagingConnectionType(source);
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
            var connectionPageInfo = Expression.Property(connectionParameter, typeof(IPagingConnection), "PageInfo");
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

            var target = GetTarget(source);

            //// Builds the final expression:
            ////
            //// source.Select(connection => new Page<TResult>
            //// {
            ////     HasNextPage = connection.PageInfo.HasNextPage,
            ////     EndCursor = connection.PageInfo.EndCursor,
            ////     Items = connection.Nodes.Select(pages.Selector).ToList(),
            //// }
            return Expression.Call(
                QueryableValueExtensions.SelectMethod.MakeGenericMethod(sourceConnectionType, pageType),
                target,
                selectPage);
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

        private Expression GetTarget(Expression expression)
        {
            if (expression is MethodCallExpression allPages &&
                allPages.Method.IsGenericMethod &&
                allPages.Method.GetGenericMethodDefinition() == PagedListExtensions.AllPagesMethod)
            {
                var queryMethod = GetQueryableValueMethod(allPages.Arguments[0]);
                var arguments = queryMethod.Arguments.ToList();
                var parameters = queryMethod.Method.GetParameters();

                for (var i = 0; i < parameters.Length; ++i)
                {
                    switch (parameters[i].Name)
                    {
                        case "first":
                            arguments[i] = Expression.Constant(
                                InsertVariables ? new Arg<int>("first", 0) : new Arg<int>(null, PageSize),
                                typeof(Arg<int>?));
                            break;
                        case "after":
                            if (InsertVariables)
                            {
                                arguments[i] = Expression.Constant(
                                    new Arg<string>("after", null),
                                    typeof(Arg<string>?));
                            }
                            break;
                    }
                }

                return queryMethod.Update(queryMethod.Object, arguments);
            }
            else
            {
                throw new NotSupportedException(
                    $"Expected an IPagingConnection<T>.AllPages() expression but got '{expression}'.");
            }
        }

        private static MethodCallExpression GetQueryableValueMethod(Expression expression)
        {
            if (expression is MethodCallExpression m &&
                typeof(IQueryableValue).GetTypeInfo().IsAssignableFrom(m.Method.DeclaringType.GetTypeInfo()))
            {
                return m;
            }
            else
            {
                throw new NotSupportedException(
                    $"Expected an IQueryableValue method expression but got '{expression}'.");
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
