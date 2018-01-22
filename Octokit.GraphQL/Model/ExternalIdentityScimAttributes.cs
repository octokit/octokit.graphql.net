namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// SCIM attributes for the External Identity
    /// </summary>
    public class ExternalIdentityScimAttributes : QueryableValue<ExternalIdentityScimAttributes>
    {
        public ExternalIdentityScimAttributes(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The userName of the SCIM identity
        /// </summary>
        public string Username { get; }

        internal static ExternalIdentityScimAttributes Create(Expression expression)
        {
            return new ExternalIdentityScimAttributes(expression);
        }
    }
}