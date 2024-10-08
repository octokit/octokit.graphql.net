namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of CreateEnvironment
    /// </summary>
    public class CreateEnvironmentInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The node ID of the repository.
        /// </summary>
        public ID RepositoryId { get; set; }

        /// <summary>
        /// The name of the environment.
        /// </summary>
        public string Name { get; set; }
    }
}