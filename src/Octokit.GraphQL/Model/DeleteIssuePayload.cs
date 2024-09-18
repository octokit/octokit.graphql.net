namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Autogenerated return type of DeleteIssue.
    /// </summary>
    public class DeleteIssuePayload : QueryableValue<DeleteIssuePayload>
    {
        internal DeleteIssuePayload(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A unique identifier for the client performing the mutation.
        /// </summary>
        public string ClientMutationId { get; }

        /// <summary>
        /// The repository the issue belonged to
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static DeleteIssuePayload Create(Expression expression)
        {
            return new DeleteIssuePayload(expression);
        }
    }
}