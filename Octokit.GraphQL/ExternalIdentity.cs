namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An external identity provisioned by SAML SSO or SCIM.
    /// </summary>
    public class ExternalIdentity : QueryEntity
    {
        public ExternalIdentity(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The GUID for this identity
        /// </summary>
        public string Guid { get; }

        public string Id { get; }

        /// <summary>
        /// Organization invitation for this SCIM-provisioned external identity
        /// </summary>
        public OrganizationInvitation OrganizationInvitation => this.CreateProperty(x => x.OrganizationInvitation, Octokit.GraphQL.OrganizationInvitation.Create);

        /// <summary>
        /// SAML Identity attributes
        /// </summary>
        public ExternalIdentitySamlAttributes SamlIdentity => this.CreateProperty(x => x.SamlIdentity, Octokit.GraphQL.ExternalIdentitySamlAttributes.Create);

        /// <summary>
        /// SCIM Identity attributes
        /// </summary>
        public ExternalIdentityScimAttributes ScimIdentity => this.CreateProperty(x => x.ScimIdentity, Octokit.GraphQL.ExternalIdentityScimAttributes.Create);

        /// <summary>
        /// User linked to this external identity
        /// </summary>
        public User User => this.CreateProperty(x => x.User, Octokit.GraphQL.User.Create);

        internal static ExternalIdentity Create(IQueryProvider provider, Expression expression)
        {
            return new ExternalIdentity(provider, expression);
        }
    }
}