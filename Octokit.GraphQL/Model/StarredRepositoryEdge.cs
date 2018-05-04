namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a starred repository.
    /// </summary>
    public class StarredRepositoryEdge : QueryableValue<StarredRepositoryEdge>
    {
        public StarredRepositoryEdge(Expression expression) : base(expression)
        {
        }

        public string Cursor { get; }

        public Repository Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// Identifies when the item was starred.
        /// </summary>
        public DateTimeOffset StarredAt { get; }

        internal static StarredRepositoryEdge Create(Expression expression)
        {
            return new StarredRepositoryEdge(expression);
        }
    }
}