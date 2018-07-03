namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A list of results that matched against a search query.
    /// </summary>
    public class SearchResultItemConnection : QueryableValue<SearchResultItemConnection>, IPagingConnection<SearchResultItem>
    {
        public SearchResultItemConnection(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The number of pieces of code that matched the search query.
        /// </summary>
        public int CodeCount { get; }

        /// <summary>
        /// A list of edges.
        /// </summary>
        public IQueryableList<SearchResultItemEdge> Edges => this.CreateProperty(x => x.Edges);

        /// <summary>
        /// The number of issues that matched the search query.
        /// </summary>
        public int IssueCount { get; }

        /// <summary>
        /// A list of nodes.
        /// </summary>
        public IQueryableList<SearchResultItem> Nodes => this.CreateProperty(x => x.Nodes);

        /// <summary>
        /// Information to aid in pagination.
        /// </summary>
        public PageInfo PageInfo => this.CreateProperty(x => x.PageInfo, Octokit.GraphQL.Model.PageInfo.Create);

        /// <summary>
        /// The number of repositories that matched the search query.
        /// </summary>
        public int RepositoryCount { get; }

        /// <summary>
        /// The number of users that matched the search query.
        /// </summary>
        public int UserCount { get; }

        /// <summary>
        /// The number of wiki pages that matched the search query.
        /// </summary>
        public int WikiCount { get; }

        IPageInfo IPagingConnection.PageInfo => PageInfo;

        internal static SearchResultItemConnection Create(Expression expression)
        {
            return new SearchResultItemConnection(expression);
        }
    }
}