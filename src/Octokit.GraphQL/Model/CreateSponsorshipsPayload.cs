namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of CreateSponsorships.
    /// </summary>
    public class CreateSponsorshipsPayload : QueryableValue<CreateSponsorshipsPayload>
    {
        internal CreateSponsorshipsPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The users and organizations who received a sponsorship.
        /// </summary>
        public IQueryableList<ISponsorable> Sponsorables => this.CreateProperty(x => x.Sponsorables);

        internal static CreateSponsorshipsPayload Create(Expression expression)
        {
            return new CreateSponsorshipsPayload(expression);
        }
    }
}