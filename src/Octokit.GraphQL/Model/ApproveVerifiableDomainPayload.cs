namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of ApproveVerifiableDomain
    /// </summary>
    public class ApproveVerifiableDomainPayload : QueryableValue<ApproveVerifiableDomainPayload>
    {
        internal ApproveVerifiableDomainPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The verifiable domain that was approved.
        /// </summary>
        public VerifiableDomain Domain => this.CreateProperty(x => x.Domain, Octokit.GraphQL.Model.VerifiableDomain.Create);

        internal static ApproveVerifiableDomainPayload Create(Expression expression)
        {
            return new ApproveVerifiableDomainPayload(expression);
        }
    }
}