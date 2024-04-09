namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of TransferIssue
    /// </summary>
    public class TransferIssueInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The Node ID of the issue to be transferred
        /// </summary>
        public ID IssueId { get; set; }

        /// <summary>
        /// The Node ID of the repository the issue should be transferred to
        /// </summary>
        public ID RepositoryId { get; set; }

        /// <summary>
        /// Whether to create labels if they don't exist in the target repository (matched by name)
        /// </summary>
        public bool? CreateLabelsIfMissing { get; set; }
    }
}