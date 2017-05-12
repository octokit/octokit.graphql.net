namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An Invitation for a user to an organization.
    /// </summary>
    public class OrganizationInvitation : QueryEntity
    {
        public OrganizationInvitation(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The email address of the user invited to the organization.
        /// </summary>
        public string Email { get; }

        public string Id { get; }

        /// <summary>
        /// The user who created the invitation.
        /// </summary>
        public User Inviter => this.CreateProperty(x => x.Inviter, Octokit.GraphQL.User.Create);

        /// <summary>
        /// The login of the user invited to the organization.
        /// </summary>
        public string Login { get; }

        /// <summary>
        /// The user's pending role in the organization (e.g. member, owner).
        /// </summary>
        public OrganizationInvitationRole Role { get; }

        internal static OrganizationInvitation Create(IQueryProvider provider, Expression expression)
        {
            return new OrganizationInvitation(provider, expression);
        }
    }
}