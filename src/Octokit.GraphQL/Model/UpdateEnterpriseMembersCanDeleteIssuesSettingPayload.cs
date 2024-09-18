namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of UpdateEnterpriseMembersCanDeleteIssuesSetting.
    /// </summary>
    public class UpdateEnterpriseMembersCanDeleteIssuesSettingPayload : QueryableValue<UpdateEnterpriseMembersCanDeleteIssuesSettingPayload>
    {
        internal UpdateEnterpriseMembersCanDeleteIssuesSettingPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The enterprise with the updated members can delete issues setting.
        /// </summary>
        public Enterprise Enterprise => this.CreateProperty(x => x.Enterprise, Octokit.GraphQL.Model.Enterprise.Create);

        /// <summary>
        /// A message confirming the result of updating the members can delete issues setting.
        /// </summary>
        public string Message { get; }

        internal static UpdateEnterpriseMembersCanDeleteIssuesSettingPayload Create(Expression expression)
        {
            return new UpdateEnterpriseMembersCanDeleteIssuesSettingPayload(expression);
        }
    }
}