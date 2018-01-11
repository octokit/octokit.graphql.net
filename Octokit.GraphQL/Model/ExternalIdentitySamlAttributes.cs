namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// SAML attributes for the External Identity
    /// </summary>
    public class ExternalIdentitySamlAttributes : QueryEntity
    {
        public ExternalIdentitySamlAttributes(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The NameID of the SAML identity
        /// </summary>
        public string NameId { get; }

        internal static ExternalIdentitySamlAttributes Create(IQueryProvider provider, Expression expression)
        {
            return new ExternalIdentitySamlAttributes(provider, expression);
        }
    }
}