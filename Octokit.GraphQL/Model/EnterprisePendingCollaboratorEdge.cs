namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A user with an invitation to be a collaborator on a repository owned by an organization in an enterprise.
    /// </summary>
    public class EnterprisePendingCollaboratorEdge : QueryableValue<EnterprisePendingCollaboratorEdge>
    {
        internal EnterprisePendingCollaboratorEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// Whether the invited collaborator does not have a license for the enterprise.
        /// </summary>
        [Obsolete(@"All pending collaborators consume a license Removal on 2021-01-01 UTC.")]
        public bool IsUnlicensed { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public User Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The enterprise organization repositories this user is a member of.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for repositories.</param>
        public EnterpriseRepositoryInfoConnection Repositories(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<RepositoryOrder>? orderBy = null) => this.CreateMethodCall(x => x.Repositories(first, after, last, before, orderBy), Octokit.GraphQL.Model.EnterpriseRepositoryInfoConnection.Create);

        internal static EnterprisePendingCollaboratorEdge Create(Expression expression)
        {
            return new EnterprisePendingCollaboratorEdge(expression);
        }
    }
}