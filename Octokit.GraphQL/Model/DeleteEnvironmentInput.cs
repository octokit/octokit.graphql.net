namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of DeleteEnvironment
    /// </summary>
    public class DeleteEnvironmentInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The Node ID of the environment to be deleted.
        /// </summary>
        public ID Id { get; set; }
    }
}