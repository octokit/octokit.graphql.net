namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a team repository.
    /// </summary>
    public class TeamRepositoryEdge : QueryableValue<TeamRepositoryEdge>
    {
        public TeamRepositoryEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Cursor { get; }

        public Repository Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The permission level the team has on the repository
        /// </summary>
        public RepositoryPermission Permission { get; }

        internal static TeamRepositoryEdge Create(IQueryProvider provider, Expression expression)
        {
            return new TeamRepositoryEdge(provider, expression);
        }
    }
}