namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of AddProjectV2ItemById
    /// </summary>
    public class AddProjectV2ItemByIdPayload : QueryableValue<AddProjectV2ItemByIdPayload>
    {
        internal AddProjectV2ItemByIdPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The item added to the project.
        /// </summary>
        public ProjectV2Item Item => this.CreateProperty(x => x.Item, Octokit.GraphQL.Model.ProjectV2Item.Create);

        internal static AddProjectV2ItemByIdPayload Create(Expression expression)
        {
            return new AddProjectV2ItemByIdPayload(expression);
        }
    }
}