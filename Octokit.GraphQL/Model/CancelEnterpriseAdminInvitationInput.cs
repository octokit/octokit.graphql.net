namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of CancelEnterpriseAdminInvitation
    /// </summary>
    public class CancelEnterpriseAdminInvitationInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The Node ID of the pending enterprise administrator invitation.
        /// </summary>
        public ID InvitationId { get; set; }
    }
}