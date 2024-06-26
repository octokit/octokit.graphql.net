namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of UnlockLockable
    /// </summary>
    public class UnlockLockableInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// ID of the item to be unlocked.
        /// </summary>
        public ID LockableId { get; set; }
    }
}