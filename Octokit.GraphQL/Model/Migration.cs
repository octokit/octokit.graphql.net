namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an Octoshift migration.
    /// </summary>
    public interface IMigration : IQueryableValue<IMigration>, IQueryableInterface
    {
        /// <summary>
        /// The Octoshift migration flag to continue on error.
        /// </summary>
        bool ContinueOnError { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The reason the migration failed.
        /// </summary>
        string FailureReason { get; }

        ID Id { get; }

        /// <summary>
        /// The Octoshift migration source.
        /// </summary>
        MigrationSource MigrationSource { get; }

        /// <summary>
        /// The target repository name.
        /// </summary>
        string RepositoryName { get; }

        /// <summary>
        /// The Octoshift migration source URL.
        /// </summary>
        string SourceUrl { get; }

        /// <summary>
        /// The Octoshift migration state.
        /// </summary>
        MigrationState State { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIMigration : QueryableValue<StubIMigration>, IMigration
    {
        internal StubIMigration(Expression expression) : base(expression)
        {
        }

        public bool ContinueOnError { get; }

        public DateTimeOffset CreatedAt { get; }

        public string FailureReason { get; }

        public ID Id { get; }

        public MigrationSource MigrationSource => this.CreateProperty(x => x.MigrationSource, Octokit.GraphQL.Model.MigrationSource.Create);

        public string RepositoryName { get; }

        public string SourceUrl { get; }

        public MigrationState State { get; }

        internal static StubIMigration Create(Expression expression)
        {
            return new StubIMigration(expression);
        }
    }
}