namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of UnmarkProjectV2AsTemplate
    /// </summary>
    public class UnmarkProjectV2AsTemplateInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The ID of the Project to unmark as a template.
        /// </summary>
        public ID ProjectId { get; set; }
    }
}