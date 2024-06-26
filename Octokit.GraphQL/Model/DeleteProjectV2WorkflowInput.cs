namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of DeleteProjectV2Workflow
    /// </summary>
    public class DeleteProjectV2WorkflowInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The ID of the workflow to be removed.
        /// </summary>
        public ID WorkflowId { get; set; }
    }
}