namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of CreateUserList
    /// </summary>
    public class CreateUserListInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The name of the new list
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A description of the list
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Whether or not the list is private
        /// </summary>
        public bool? IsPrivate { get; set; }
    }
}