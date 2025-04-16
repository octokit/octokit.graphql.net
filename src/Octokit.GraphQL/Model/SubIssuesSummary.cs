using System.Linq.Expressions;
using Octokit.GraphQL.Core;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Summary of the state of an issue's sub-issues
    /// </summary>
    public class SubIssuesSummary : QueryableValue<SubIssuesSummary>
    {
        internal SubIssuesSummary(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Count of completed sub-issues
        /// </summary>
        public int Completed { get; }

        /// <summary>
        /// Percent of sub-issues which are completed
        /// </summary>
        public int percentCompleted { get; }

        /// <summary>
        /// Count of total number of sub-issues
        /// </summary>
        public int Total { get; }

        internal static SubIssuesSummary Create(Expression expression)
        {
            return new SubIssuesSummary(expression);
        }
    }
}