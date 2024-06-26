namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of UpdateTopics
    /// </summary>
    public class UpdateTopicsInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The Node ID of the repository.
        /// </summary>
        public ID RepositoryId { get; set; }

        /// <summary>
        /// An array of topic names.
        /// </summary>
        public IEnumerable<string> TopicNames { get; set; }
    }
}