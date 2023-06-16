namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of UpdateRepositoryWebCommitSignoffSetting
    /// </summary>
    public class UpdateRepositoryWebCommitSignoffSettingPayload : QueryableValue<UpdateRepositoryWebCommitSignoffSettingPayload>
    {
        internal UpdateRepositoryWebCommitSignoffSettingPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// A message confirming the result of updating the web commit signoff setting.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// The updated repository.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static UpdateRepositoryWebCommitSignoffSettingPayload Create(Expression expression)
        {
            return new UpdateRepositoryWebCommitSignoffSettingPayload(expression);
        }
    }
}