namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// A team of users in an organization.
    /// </summary>
    public class Team : QueryEntity
    {
        public Team(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The description of the team.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The HTTP path for editing this team
        /// </summary>
        public string EditTeamPath { get; }

        /// <summary>
        /// The HTTP url for editing this team
        /// </summary>
        public string EditTeamUrl { get; }

        public string Id { get; }

        /// <summary>
        /// A list of pending invitations for users to this team
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public OrganizationInvitationConnection Invitations(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.Invitations(first, after, last, before), Octoqit.OrganizationInvitationConnection.Create);

        /// <summary>
        /// The name of the team.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The path to this resource.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// The level of privacy the team has.
        /// </summary>
        public TeamPrivacy Privacy { get; }

        /// <summary>
        /// The slug corresponding to the team.
        /// </summary>
        public string Slug { get; }

        /// <summary>
        /// The HTTP path for this team
        /// </summary>
        public string TeamPath { get; }

        /// <summary>
        /// The HTTP path for this team's repositories
        /// </summary>
        public string TeamRepositoriesPath { get; }

        /// <summary>
        /// The HTTP url for this team's repositories
        /// </summary>
        public string TeamRepositoriesUrl { get; }

        /// <summary>
        /// The HTTP url for this team
        /// </summary>
        public string TeamUrl { get; }

        /// <summary>
        /// The URL to this resource.
        /// </summary>
        public string Url { get; }

        internal static Team Create(IQueryProvider provider, Expression expression)
        {
            return new Team(provider, expression);
        }
    }
}