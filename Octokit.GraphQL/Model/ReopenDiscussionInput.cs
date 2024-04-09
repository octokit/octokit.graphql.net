namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of ReopenDiscussion
    /// </summary>
    public class ReopenDiscussionInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// ID of the discussion to be reopened.
        /// </summary>
        public ID DiscussionId { get; set; }
    }
}