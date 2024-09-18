namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of CreateIpAllowListEntry.
    /// </summary>
    public class CreateIpAllowListEntryPayload : QueryableValue<CreateIpAllowListEntryPayload>
    {
        internal CreateIpAllowListEntryPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The IP allow list entry that was created.
        /// </summary>
        public IpAllowListEntry IpAllowListEntry => this.CreateProperty(x => x.IpAllowListEntry, Octokit.GraphQL.Model.IpAllowListEntry.Create);

        internal static CreateIpAllowListEntryPayload Create(Expression expression)
        {
            return new CreateIpAllowListEntryPayload(expression);
        }
    }
}