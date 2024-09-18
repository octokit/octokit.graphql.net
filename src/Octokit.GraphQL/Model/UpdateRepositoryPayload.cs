namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of UpdateRepository.
    /// </summary>
    public class UpdateRepositoryPayload : QueryableValue<UpdateRepositoryPayload>
    {
        internal UpdateRepositoryPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The updated repository.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static UpdateRepositoryPayload Create(Expression expression)
        {
            return new UpdateRepositoryPayload(expression);
        }
    }
}