namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Prevent commits that include files with specified file extensions from being pushed to the commit graph.
    /// </summary>
    public class FileExtensionRestrictionParameters : QueryableValue<FileExtensionRestrictionParameters>
    {
        internal FileExtensionRestrictionParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The file extensions that are restricted from being pushed to the commit graph.
        /// </summary>
        public IEnumerable<string> RestrictedFileExtensions { get; }

        internal static FileExtensionRestrictionParameters Create(Expression expression)
        {
            return new FileExtensionRestrictionParameters(expression);
        }
    }
}