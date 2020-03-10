using System;
using System.Linq.Expressions;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class SearchResultItemConnection : QueryableValue<SearchResultItemConnection>, IPagingConnection<SearchResultItem>
    {
        internal SearchResultItemConnection(Expression expression) : base(expression)
        {
        }

        public IQueryableList<SearchResultItem> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Models.PageInfo.Create);

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static SearchResultItemConnection Create(Expression expression)
        {
            return new SearchResultItemConnection(expression);
        }
    }
}