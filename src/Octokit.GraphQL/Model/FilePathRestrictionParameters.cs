namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Prevent commits that include changes in specified file paths from being pushed to the commit graph.
    /// </summary>
    public class FilePathRestrictionParameters : QueryableValue<FilePathRestrictionParameters>
    {
        internal FilePathRestrictionParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The file paths that are restricted from being pushed to the commit graph.
        /// </summary>
        public IEnumerable<string> RestrictedFilePaths { get; }

        internal static FilePathRestrictionParameters Create(Expression expression)
        {
            return new FilePathRestrictionParameters(expression);
        }
    }
}