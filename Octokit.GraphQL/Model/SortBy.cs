namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a sort by field and direction.
    /// </summary>
    public class SortBy : QueryableValue<SortBy>
    {
        internal SortBy(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The direction of the sorting. Possible values are ASC and DESC.
        /// </summary>
        public OrderDirection Direction { get; }

        /// <summary>
        /// The id of the field by which the column is sorted.
        /// </summary>
        public int Field { get; }

        internal static SortBy Create(Expression expression)
        {
            return new SortBy(expression);
        }
    }
}