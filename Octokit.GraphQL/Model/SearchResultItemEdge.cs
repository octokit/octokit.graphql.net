namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An edge in a connection.
    /// </summary>
    public class SearchResultItemEdge : QueryEntity
    {
        public SearchResultItemEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public SearchResultItem Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.SearchResultItem.Create);

        /// <summary>
        /// Text matches on the result found.
        /// </summary>
        public IQueryable<TextMatch> TextMatches => this.CreateProperty(x => x.TextMatches);

        internal static SearchResultItemEdge Create(IQueryProvider provider, Expression expression)
        {
            return new SearchResultItemEdge(provider, expression);
        }
    }
}