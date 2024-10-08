namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of AddProjectV2DraftIssue
    /// </summary>
    public class AddProjectV2DraftIssuePayload : QueryableValue<AddProjectV2DraftIssuePayload>
    {
        internal AddProjectV2DraftIssuePayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The draft issue added to the project.
        /// </summary>
        public ProjectV2Item ProjectItem => this.CreateProperty(x => x.ProjectItem, Octokit.GraphQL.Model.ProjectV2Item.Create);

        internal static AddProjectV2DraftIssuePayload Create(Expression expression)
        {
            return new AddProjectV2DraftIssuePayload(expression);
        }
    }
}