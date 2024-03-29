namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of UnlinkProjectV2FromRepository
    /// </summary>
    public class UnlinkProjectV2FromRepositoryPayload : QueryableValue<UnlinkProjectV2FromRepositoryPayload>
    {
        internal UnlinkProjectV2FromRepositoryPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The repository the project is no longer linked to.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static UnlinkProjectV2FromRepositoryPayload Create(Expression expression)
        {
            return new UnlinkProjectV2FromRepositoryPayload(expression);
        }
    }
}