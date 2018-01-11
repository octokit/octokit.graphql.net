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
    public class IssueEdge : QueryEntity
    {
        public IssueEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Issue Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.Issue.Create);

        internal static IssueEdge Create(IQueryProvider provider, Expression expression)
        {
            return new IssueEdge(provider, expression);
        }
    }
}