using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core
{
    public class PagedSubquery<TResult> : PagedQuery<TResult>, ISubquery
    {
        public PagedSubquery(
            SimpleQuery<TResult> masterQuery,
            IEnumerable<ISubquery> subqueries,
            Expression<Func<JObject, IEnumerable<JToken>>> parentIds,
            Expression<Func<JObject, JToken>> pageInfo,
            Expression<Func<JObject, IEnumerable<JToken>>> parentPageInfo)
            : base(masterQuery, subqueries)
        {
            ParentIds = ExpressionCompiler.Compile(parentIds);
            PageInfo = ExpressionCompiler.Compile(pageInfo);
            ParentPageInfo = ExpressionCompiler.Compile(parentPageInfo);
        }

        public Func<JObject, IEnumerable<JToken>> ParentIds { get; }

        public Func<JObject, JToken> PageInfo { get; }

        public Func<JObject, IEnumerable<JToken>> ParentPageInfo { get; }

        public IQueryRunner Start(
            IConnection connection,
            string id,
            string after,
            IDictionary<string, object> variables)
        {
            throw new NotImplementedException();
        }

        public static ISubquery Create(
            Type resultType,
            ICompiledQuery masterQuery,
            IEnumerable<ISubquery> subqueries,
            Expression<Func<JObject, IEnumerable<JToken>>> parentIds,
            Expression<Func<JObject, JToken>> pageInfo,
            Expression<Func<JObject, IEnumerable<JToken>>> parentPageInfo)
        {
            var ctor = typeof(PagedSubquery<>)
                .MakeGenericType(resultType)
                .GetTypeInfo()
                .DeclaredConstructors
                .Single();

            return (ISubquery)ctor.Invoke(new object[]
            {
                masterQuery,
                subqueries,
                parentIds,
                pageInfo,
                parentPageInfo
            });
        }
    }
}
