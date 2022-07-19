namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An Octoshift repository migration.
    /// </summary>
    public class RepositoryMigration : QueryableValue<RepositoryMigration>
    {
        internal RepositoryMigration(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The Octoshift migration flag to continue on error.
        /// </summary>
        public bool ContinueOnError { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The reason the migration failed.
        /// </summary>
        public string FailureReason { get; }

        public ID Id { get; }

        /// <summary>
        /// The URL for the migration log (expires 1 day after migration completes).
        /// </summary>
        public string MigrationLogUrl { get; }

        /// <summary>
        /// The Octoshift migration source.
        /// </summary>
        public MigrationSource MigrationSource => this.CreateProperty(x => x.MigrationSource, Octokit.GraphQL.Model.MigrationSource.Create);

        /// <summary>
        /// The target repository name.
        /// </summary>
        public string RepositoryName { get; }

        /// <summary>
        /// The Octoshift migration source URL.
        /// </summary>
        public string SourceUrl { get; }

        /// <summary>
        /// The Octoshift migration state.
        /// </summary>
        public MigrationState State { get; }

        internal static RepositoryMigration Create(Expression expression)
        {
            return new RepositoryMigration(expression);
        }
    }
}