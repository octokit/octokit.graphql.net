namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of VerifyVerifiableDomain
    /// </summary>
    public class VerifyVerifiableDomainInput
    {
        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; set; }

        /// <summary>
        /// The ID of the verifiable domain to verify.
        /// </summary>
        public ID Id { get; set; }
    }
}