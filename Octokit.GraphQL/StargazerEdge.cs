namespace Octokit.GraphQL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a user that's starred a repository.
    /// </summary>
    public class StargazerEdge : QueryEntity
    {
        public StargazerEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Cursor { get; }

        public User Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.User.Create);

        /// <summary>
        /// Identifies when the item was starred.
        /// </summary>
        public DateTimeOffset? StarredAt { get; }

        internal static StargazerEdge Create(IQueryProvider provider, Expression expression)
        {
            return new StargazerEdge(provider, expression);
        }
    }
}