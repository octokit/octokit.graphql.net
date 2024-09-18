namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of ConvertProjectV2DraftIssueItemToIssue
    /// </summary>
    public class ConvertProjectV2DraftIssueItemToIssueInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The ID of the draft issue ProjectV2Item to convert.
        /// </summary>
        public ID ItemId { get; set; }

        /// <summary>
        /// The ID of the repository to create the issue in.
        /// </summary>
        public ID RepositoryId { get; set; }
    }
}