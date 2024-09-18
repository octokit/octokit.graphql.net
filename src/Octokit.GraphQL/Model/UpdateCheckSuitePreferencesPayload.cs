namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of UpdateCheckSuitePreferences.
    /// </summary>
    public class UpdateCheckSuitePreferencesPayload : QueryableValue<UpdateCheckSuitePreferencesPayload>
    {
        internal UpdateCheckSuitePreferencesPayload(Expression expression) : base(expression)
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

        internal static UpdateCheckSuitePreferencesPayload Create(Expression expression)
        {
            return new UpdateCheckSuitePreferencesPayload(expression);
        }
    }
}