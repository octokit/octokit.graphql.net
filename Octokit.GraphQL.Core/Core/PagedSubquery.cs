using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core
{
    /// <summary>
    /// A sub-query of a <see cref="PagedQuery{TResult}"/> which is itself paged.
    /// </summary>
    /// <typeparam name="TResult">The query result type.</typeparam>
    public class PagedSubquery<TResult> : PagedQuery<TResult>, ISubquery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleQuery{TResult}"/> class.
        /// </summary>
        /// <param name="masterQuery">The master query.</param>
        /// <param name="subqueries">The sub-queries.</param>
        /// <param name="parentIds">
        /// A function which given the data from the master query, returns the IDs of the
        /// entities which can be auto-paged.
        /// </param>
        /// <param name="pageInfo">
        /// A function which given the data from the sub-query, returns the paging info.
        /// </param>
        /// <param name="parentPageInfo">
        /// A function which given the data from the master query, returns the paging info
        /// for all entities which can be auto-paged.
        /// </param>
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

        /// <summary>
        /// Gets a function which given the data from the master query, returns the IDs of the
        /// entities which can be auto-paged.
        /// </summary>
        public Func<JObject, IEnumerable<JToken>> ParentIds { get; }

        /// <summary>
        /// Gets a function which given the data from the sub-query, returns the paging info.
        /// </summary>
        public Func<JObject, JToken> PageInfo { get; }

        /// <summary>
        /// Gets a function which given the data from the master query, returns the paging info
        /// for all entities which can be auto-paged.
        /// </summary>
        public Func<JObject, IEnumerable<JToken>> ParentPageInfo { get; }

        /// <inheritdoc/>
        public IQueryRunner Start(
            IConnection connection,
            string id,
            string after,
            IDictionary<string, object> variables,
            IList result)
        {
            throw new NotImplementedException();
        }

        internal static ISubquery Create(
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
