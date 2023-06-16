namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of UnlinkProjectV2FromTeam
    /// </summary>
    public class UnlinkProjectV2FromTeamInput
    {
        /// <summary>
        /// The ID of the project to unlink from the team.
        /// </summary>
        public ID ProjectId { get; set; }

        /// <summary>
        /// The ID of the team to unlink from the project.
        /// </summary>
        public ID TeamId { get; set; }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }
    }
}