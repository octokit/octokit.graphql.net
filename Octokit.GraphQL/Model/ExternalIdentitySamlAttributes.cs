namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// SAML attributes for the External Identity
    /// </summary>
    public class ExternalIdentitySamlAttributes : QueryableValue<ExternalIdentitySamlAttributes>
    {
        public ExternalIdentitySamlAttributes(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The NameID of the SAML identity
        /// </summary>
        public string NameId { get; }

        internal static ExternalIdentitySamlAttributes Create(Expression expression)
        {
            return new ExternalIdentitySamlAttributes(expression);
        }
    }
}