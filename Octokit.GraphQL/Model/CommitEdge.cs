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
    public class CommitEdge : QueryableValue<CommitEdge>
    {
        public CommitEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public Commit Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.Commit.Create);

        internal static CommitEdge Create(IQueryProvider provider, Expression expression)
        {
            return new CommitEdge(provider, expression);
        }
    }
}