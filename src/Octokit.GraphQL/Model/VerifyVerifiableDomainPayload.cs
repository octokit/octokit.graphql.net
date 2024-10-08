namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of VerifyVerifiableDomain
    /// </summary>
    public class VerifyVerifiableDomainPayload : QueryableValue<VerifyVerifiableDomainPayload>
    {
        internal VerifyVerifiableDomainPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The verifiable domain that was verified.
        /// </summary>
        public VerifiableDomain Domain => this.CreateProperty(x => x.Domain, Octokit.GraphQL.Model.VerifiableDomain.Create);

        internal static VerifyVerifiableDomainPayload Create(Expression expression)
        {
            return new VerifyVerifiableDomainPayload(expression);
        }
    }
}