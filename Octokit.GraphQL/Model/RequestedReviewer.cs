namespace Octokit.GraphQL.Model
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Types that can be requested reviewers.
    /// </summary>
    public class RequestedReviewer : QueryableValue<RequestedReviewer>, IUnion
    {
        public RequestedReviewer(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A user is an individual's account on GitHub that owns repositories and can make new content.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// A team of users in an organization.
        /// </summary>
        public Team Team => this.CreateProperty(x => x.Team, Octokit.GraphQL.Model.Team.Create);

        internal static RequestedReviewer Create(Expression expression)
        {
            return new RequestedReviewer(expression);
        }
    }
}