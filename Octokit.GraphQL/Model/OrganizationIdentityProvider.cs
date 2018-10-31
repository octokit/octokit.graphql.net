namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// An Identity Provider configured to provision SAML and SCIM identities for Organizations
    /// </summary>
    public class OrganizationIdentityProvider : QueryableValue<OrganizationIdentityProvider>
    {
        public OrganizationIdentityProvider(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The digest algorithm used to sign SAML requests for the Identity Provider.
        /// </summary>
        public string DigestMethod { get; }

        /// <summary>
        /// External Identities provisioned by this Identity Provider
        /// </summary>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        public ExternalIdentityConnection ExternalIdentities(Arg<string>? after = null, Arg<string>? before = null, Arg<int>? first = null, Arg<int>? last = null) => this.CreateMethodCall(x => x.ExternalIdentities(after, before, first, last), Octokit.GraphQL.Model.ExternalIdentityConnection.Create);

        public ID Id { get; }

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
        public Organization Organization => this.CreateProperty(x => x.Organization, Octokit.GraphQL.Model.Organization.Create);

        /// <summary>
        /// The signature algorithm used to sign SAML requests for the Identity Provider.
        /// </summary>
        public string SignatureMethod { get; }

        /// <summary>
        /// The URL endpoint for the Identity Provider's SAML SSO.
        /// </summary>
        public string SsoUrl { get; }

        internal static OrganizationIdentityProvider Create(Expression expression)
        {
            return new OrganizationIdentityProvider(expression);
        }
    }
}