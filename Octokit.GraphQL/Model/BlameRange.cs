namespace Octokit.GraphQL.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents a range of information from a Git blame.
    /// </summary>
    public class BlameRange : QueryEntity
    {
        public BlameRange(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Identifies the recency of the change, from 1 (new) to 10 (old). This is calculated as a 2-quantile and determines the length of distance between the median age of all the changes in the file and the recency of the current range's change.
        /// </summary>
        public int Age { get; }

        /// <summary>
        /// Identifies the line author
        /// </summary>
        public Commit Commit => this.CreateProperty(x => x.Commit, Octokit.GraphQL.Model.Commit.Create);

        /// <summary>
        /// The ending line for the range
        /// </summary>
        public int EndingLine { get; }

        /// <summary>
        /// The starting line for the range
        /// </summary>
        public int StartingLine { get; }

        internal static BlameRange Create(IQueryProvider provider, Expression expression)
        {
            return new BlameRange(provider, expression);
        }
    }
}