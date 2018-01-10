namespace Octokit.GraphQL.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a user who is a collaborator of a repository.
    /// </summary>
    public class RepositoryCollaboratorEdge : QueryEntity
    {
        public RepositoryCollaboratorEdge(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Cursor { get; }

        public User Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The permission the user has on the repository.
        /// </summary>
        public RepositoryPermission Permission { get; }

        internal static RepositoryCollaboratorEdge Create(IQueryProvider provider, Expression expression)
        {
            return new RepositoryCollaboratorEdge(provider, expression);
        }
    }
}