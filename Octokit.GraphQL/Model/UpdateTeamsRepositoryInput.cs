namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of UpdateTeamsRepository
    /// </summary>
    public class UpdateTeamsRepositoryInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// Repository ID being granted access to.
        /// </summary>
        public ID RepositoryId { get; set; }

        /// <summary>
        /// A list of teams being granted access. Limit: 10
        /// </summary>
        public IEnumerable<ID> TeamIds { get; set; }

        /// <summary>
        /// Permission that should be granted to the teams.
        /// </summary>
        public RepositoryPermission Permission { get; set; }
    }
}