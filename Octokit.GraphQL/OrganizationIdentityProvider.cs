namespace Octokit.GraphQL
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An Identity Provider configured to provision SAML and SCIM identities for Organizations
    /// </summary>
    public class OrganizationIdentityProvider : QueryEntity
    {
        public OrganizationIdentityProvider(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The digest algorithm used to sign SAML requests for the Identity Provider.
        /// </summary>
        public string DigestMethod { get; }

        /// <summary>
        /// External Identities provisioned by this Identity Provider
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified global ID.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified global ID.</param>
        public ExternalIdentityConnection ExternalIdentities(int? first = null, string after = null, int? last = null, string before = null) => this.CreateMethodCall(x => x.ExternalIdentities(first, after, last, before), Octokit.GraphQL.ExternalIdentityConnection.Create);

        public string Id { get; }

        /// <summary>
        /// The x509 certificate used by the Identity Provder to sign assertions and responses.
        /// </summary>
        public string IdpCertificate { get; }

        /// <summary>
        /// The Issuer Entity ID for the SAML Identity Provider
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// Organization this Identity Provider belongs to
        /// </summary>
        public Organization Organization => this.CreateProperty(x => x.Organization, Octokit.GraphQL.Organization.Create);

        /// <summary>
        /// The signature algorithm used to sign SAML requests for the Identity Provider.
        /// </summary>
        public string SignatureMethod { get; }

        /// <summary>
        /// The URL endpoint for the Identity Provider's SAML SSO.
        /// </summary>
        public string SsoUrl { get; }

        internal static OrganizationIdentityProvider Create(IQueryProvider provider, Expression expression)
        {
            return new OrganizationIdentityProvider(provider, expression);
        }
    }
}