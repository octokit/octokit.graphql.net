namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a user within an organization.
    /// </summary>
    public class OrganizationMemberEdge : QueryableValue<OrganizationMemberEdge>
    {
        public OrganizationMemberEdge(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A cursor for use in pagination.
        /// </summary>
        public string Cursor { get; }

        /// <summary>
        /// The item at the end of the edge.
        /// </summary>
        public User Node => this.CreateProperty(x => x.Node, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The role this user has in the organization.
        /// </summary>
        public OrganizationMemberRole? Role { get; }

        internal static OrganizationMemberEdge Create(Expression expression)
        {
            return new OrganizationMemberEdge(expression);
        }
    }
}