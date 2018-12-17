namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A content reference
    /// </summary>
    public class ContentReference : QueryableValue<ContentReference>
    {
        internal ContentReference(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int DatabaseId { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide summary")]
        public ID Id { get; }

        /// <summary>
        /// The reference of the content reference.
        /// </summary>
        public string Reference { get; }

        internal static ContentReference Create(Expression expression)
        {
            return new ContentReference(expression);
        }
    }
}