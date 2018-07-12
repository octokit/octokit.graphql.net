namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A single check annotation.
    /// </summary>
    public class CheckAnnotation : QueryableValue<CheckAnnotation>
    {
        public CheckAnnotation(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The path to the file that this annotation was made on.
        /// </summary>
        public string BlobUrl { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public int? DatabaseId { get; }

        /// <summary>
        /// The ending line for this annotation.
        /// </summary>
        public int EndLine { get; }

        /// <summary>
        /// The filename that this annotation was made on.
        /// </summary>
        public string Filename { get; }

        /// <summary>
        /// The annotation's message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Additional information about the annotation.
        /// </summary>
        public string RawDetails { get; }

        /// <summary>
        /// The starting line for this annotation.
        /// </summary>
        public int StartLine { get; }

        /// <summary>
        /// The annotation's title
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// The annotation's severity level.
        /// </summary>
        public CheckAnnotationLevel? WarningLevel { get; }

        internal static CheckAnnotation Create(Expression expression)
        {
            return new CheckAnnotation(expression);
        }
    }
}