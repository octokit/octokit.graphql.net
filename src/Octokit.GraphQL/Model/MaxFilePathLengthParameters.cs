namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Prevent commits that include file paths that exceed a specified character limit from being pushed to the commit graph.
    /// </summary>
    public class MaxFilePathLengthParameters : QueryableValue<MaxFilePathLengthParameters>
    {
        internal MaxFilePathLengthParameters(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The maximum amount of characters allowed in file paths
        /// </summary>
        public int MaxFilePathLength { get; }

        internal static MaxFilePathLengthParameters Create(Expression expression)
        {
            return new MaxFilePathLengthParameters(expression);
        }
    }
}