namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a user who is a collaborator of a repository.
    /// </summary>
    public class RepositoryCollaboratorEdge : QueryableValue<RepositoryCollaboratorEdge>
    {
        internal RepositoryCollaboratorEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public User Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The permission the user has on the repository.
        /// </summary>
        public RepositoryPermission Permission { get; }

        internal static RepositoryCollaboratorEdge Create(Expression expression)
        {
            return new RepositoryCollaboratorEdge(expression);
        }
    }
}