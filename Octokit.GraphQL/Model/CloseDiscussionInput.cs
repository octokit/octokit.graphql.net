namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of CloseDiscussion
    /// </summary>
    public class CloseDiscussionInput
    {
        /// <summary>
        /// ID of the discussion to be closed.
        /// </summary>
        public ID DiscussionId { get; set; }

        /// <summary>
        /// The reason why the discussion is being closed.
        /// </summary>
        public DiscussionCloseReason? Reason { get; set; }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }
    }
}