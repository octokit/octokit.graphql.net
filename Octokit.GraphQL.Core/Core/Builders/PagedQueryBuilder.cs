using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Octokit.GraphQL.Core.Builders
{
    public class PagedQueryBuilder
    {
        public CompiledQuery<IEnumerable<T>> Build<T>(IPagedList<T> pages)
            where T : IQueryableList
        {
            throw new NotImplementedException();
        }

        public Expression BuildPageExpression<T>(IPagedList<T> pages)
            where T : IQueryableList
        {
            var sourceType = pages.Method.ReturnType.GenericTypeArguments[0];
            var resultType = typeof(T).GenericTypeArguments[0];

            var listSelect = QueryableListExtensions.SelectMethod
                .MakeGenericMethod(sourceType, resultType);

            var args = BuildPagingVariables(pages.Method).ToList();
            var pagingMethod = Expression.Call(
                pages.Expression,
                pages.Method,
                BuildPagingVariables(pages.Method));

            return Expression.Call(
                listSelect,
                pagingMethod,
                pages.Selector);
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
