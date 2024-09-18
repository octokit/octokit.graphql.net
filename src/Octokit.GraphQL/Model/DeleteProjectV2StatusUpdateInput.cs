namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of DeleteProjectV2StatusUpdate
    /// </summary>
    public class DeleteProjectV2StatusUpdateInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The ID of the status update to be removed.
        /// </summary>
        public ID StatusUpdateId { get; set; }
    }
}