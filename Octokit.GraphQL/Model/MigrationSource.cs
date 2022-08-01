namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An Octoshift migration source.
    /// </summary>
    public class MigrationSource : QueryableValue<MigrationSource>
    {
        internal MigrationSource(Expression expression) : base(expression)
        {
        }

        public ID Id { get; }

        /// <summary>
        /// The Octoshift migration source name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The Octoshift migration source type.
        /// </summary>
        public MigrationSourceType Type { get; }

        /// <summary>
        /// The Octoshift migration source URL.
        /// </summary>
        public string Url { get; }

        internal static MigrationSource Create(Expression expression)
        {
            return new MigrationSource(expression);
        }
    }
}