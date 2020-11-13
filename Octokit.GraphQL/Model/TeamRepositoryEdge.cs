namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a team repository.
    /// </summary>
    public class TeamRepositoryEdge : QueryableValue<TeamRepositoryEdge>
    {
        internal TeamRepositoryEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        public Repository Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.Repository.Create);

        /// <summary>
        /// The permission level the team has on the repository
        /// **Upcoming Change on 2020-10-01 UTC**
        /// **Description:** Type for `permission` will change from `RepositoryPermission!` to `String`.
        /// **Reason:** This field may return additional values
        /// </summary>
        public RepositoryPermission Permission { get; }

        internal static TeamRepositoryEdge Create(Expression expression)
        {
            return new TeamRepositoryEdge(expression);
        }
    }
}