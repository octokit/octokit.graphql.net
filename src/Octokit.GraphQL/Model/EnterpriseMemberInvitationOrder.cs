namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ordering options for enterprise administrator invitation connections
    /// </summary>
    public class EnterpriseMemberInvitationOrder
    {
        /// <summary>
        /// The field to order enterprise member invitations by.
        /// </summary>
        public EnterpriseMemberInvitationOrderField Field { get; set; }

        /// <summary>
        /// The ordering direction.
        /// </summary>
        public OrderDirection Direction { get; set; }
    }
}