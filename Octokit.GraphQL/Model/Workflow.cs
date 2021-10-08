namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A workflow contains meta information about an Actions workflow file.
    /// </summary>
    public class Workflow : QueryableValue<Workflow>
    {
        internal Workflow(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        public ID Id { get; }

        /// <summary>
        /// The name of the workflow.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static Workflow Create(Expression expression)
        {
            return new Workflow(expression);
        }
    }
}