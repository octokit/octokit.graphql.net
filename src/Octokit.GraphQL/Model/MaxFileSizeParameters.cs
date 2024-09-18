namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Prevent commits that exceed a specified file size limit from being pushed to the commit.
    /// </summary>
    public class MaxFileSizeParameters : QueryableValue<MaxFileSizeParameters>
    {
        internal MaxFileSizeParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The maximum file size allowed in megabytes. This limit does not apply to Git Large File Storage (Git LFS).
        /// </summary>
        public int MaxFileSize { get; }

        internal static MaxFileSizeParameters Create(Expression expression)
        {
            return new MaxFileSizeParameters(expression);
        }
    }
}