namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of ImportProject.
    /// </summary>
    public class ImportProjectPayload : QueryableValue<ImportProjectPayload>
    {
        internal ImportProjectPayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The new Project!
        /// </summary>
        public Project Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.Project.Create);

        internal static ImportProjectPayload Create(Expression expression)
        {
            return new ImportProjectPayload(expression);
        }
    }
}