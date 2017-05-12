namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A suggestion to review a pull request based on a user's commit history and review comments.
    /// </summary>
    public class SuggestedReviewer : QueryEntity
    {
        public SuggestedReviewer(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Is this suggestion based on past commits?
        /// </summary>
        public bool IsAuthor { get; }

        /// <summary>
        /// Is this suggestion based on past review comments?
        /// </summary>
        public bool IsCommenter { get; }

        /// <summary>
        /// Identifies the user suggested to review the pull request.
        /// </summary>
        public User Reviewer => this.CreateProperty(x => x.Reviewer, Octokit.GraphQL.User.Create);

        internal static SuggestedReviewer Create(IQueryProvider provider, Expression expression)
        {
            return new SuggestedReviewer(provider, expression);
        }
    }
}