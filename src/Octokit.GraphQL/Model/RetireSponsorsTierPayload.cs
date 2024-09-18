namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of RetireSponsorsTier.
    /// </summary>
    public class RetireSponsorsTierPayload : QueryableValue<RetireSponsorsTierPayload>
    {
        internal RetireSponsorsTierPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The tier that was retired.
        /// </summary>
        public SponsorsTier SponsorsTier => this.CreateProperty(x => x.SponsorsTier, Octokit.GraphQL.Model.SponsorsTier.Create);

        internal static RetireSponsorsTierPayload Create(Expression expression)
        {
            return new RetireSponsorsTierPayload(expression);
        }
    }
}