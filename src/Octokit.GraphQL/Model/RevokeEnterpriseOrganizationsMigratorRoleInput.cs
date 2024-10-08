namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of RevokeEnterpriseOrganizationsMigratorRole
    /// </summary>
    public class RevokeEnterpriseOrganizationsMigratorRoleInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The ID of the enterprise to which all organizations managed by it will be granted the migrator role.
        /// </summary>
        public ID EnterpriseId { get; set; }

        /// <summary>
        /// The login of the user to revoke the migrator role
        /// </summary>
        public string Login { get; set; }
    }
}