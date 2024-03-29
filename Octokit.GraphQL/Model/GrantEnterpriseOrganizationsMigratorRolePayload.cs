namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of GrantEnterpriseOrganizationsMigratorRole
    /// </summary>
    public class GrantEnterpriseOrganizationsMigratorRolePayload : QueryableValue<GrantEnterpriseOrganizationsMigratorRolePayload>
    {
        internal GrantEnterpriseOrganizationsMigratorRolePayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The organizations that had the migrator role applied to for the given user.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public OrganizationConnection Organizations(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Organizations(first, after, last, before), Octokit.GraphQL.Model.OrganizationConnection.Create);

        internal static GrantEnterpriseOrganizationsMigratorRolePayload Create(Expression expression)
        {
            return new GrantEnterpriseOrganizationsMigratorRolePayload(expression);
        }
    }
}