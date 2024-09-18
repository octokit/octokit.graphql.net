namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of DeleteVerifiableDomain.
    /// </summary>
    public class DeleteVerifiableDomainPayload : QueryableValue<DeleteVerifiableDomainPayload>
    {
        internal DeleteVerifiableDomainPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The owning account from which the domain was deleted.
        /// </summary>
        public VerifiableDomainOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.VerifiableDomainOwner.Create);

        internal static DeleteVerifiableDomainPayload Create(Expression expression)
        {
            return new DeleteVerifiableDomainPayload(expression);
        }
    }
}