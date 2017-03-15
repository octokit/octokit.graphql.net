namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Types that can be an actor.
    /// </summary>
    public class ReviewDismissalAllowanceActor : QueryEntity, IUnion
    {
        public ReviewDismissalAllowanceActor(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// A user is an individual's account on GitHub that owns repositories and can make new content.
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octoqit.User.Create);

        /// <summary>
        /// A team of users in an organization.
        /// </summary>
        public Team Team => this.CreateProperty(x => x.Team, Octoqit.Team.Create);

        internal static ReviewDismissalAllowanceActor Create(IQueryProvider provider, Expression expression)
        {
            return new ReviewDismissalAllowanceActor(provider, expression);
        }
    }
}