namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a pinned environment on a given repository
    /// </summary>
    public class PinnedEnvironment : QueryableValue<PinnedEnvironment>
    {
        internal PinnedEnvironment(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the pinned environment was created
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// Identifies the environment associated.
        /// </summary>
        public Environment Environment => this.CreateProperty(x => x.Environment, Octokit.GraphQL.Model.Environment.Create);

        /// <summary>
        /// The Node ID of the PinnedEnvironment object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// Identifies the position of the pinned environment.
        /// </summary>
        public int Position { get; }

        /// <summary>
        /// The repository that this environment was pinned to.
        /// </summary>
        public Repository Repository => this.CreateProperty(x => x.Repository, Octokit.GraphQL.Model.Repository.Create);

        internal static PinnedEnvironment Create(Expression expression)
        {
            return new PinnedEnvironment(expression);
        }
    }
}