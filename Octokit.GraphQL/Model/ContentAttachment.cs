namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A content attachment
    /// </summary>
    public class ContentAttachment : QueryableValue<ContentAttachment>
    {
        internal ContentAttachment(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The body text of the content attachment. This parameter supports markdown.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The content reference that the content attachment is attached to.
        /// </summary>
        public ContentReference ContentReference => this.CreateProperty(x => x.ContentReference, Octokit.GraphQL.Model.ContentReference.Create);

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int DatabaseId { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public ID Id { get; }

        /// <summary>
        /// The title of the content attachment.
        /// </summary>
        public string Title { get; }

        internal static ContentAttachment Create(Expression expression)
        {
            return new ContentAttachment(expression);
        }
    }
}