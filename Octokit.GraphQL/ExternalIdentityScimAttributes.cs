namespace Octokit.GraphQL
{
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// SCIM attributes for the External Identity
    /// </summary>
    public class ExternalIdentityScimAttributes : QueryEntity
    {
        public ExternalIdentityScimAttributes(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// The userName of the SCIM identity
        /// </summary>
        public string Username { get; }

        internal static ExternalIdentityScimAttributes Create(IQueryProvider provider, Expression expression)
        {
            return new ExternalIdentityScimAttributes(provider, expression);
        }
    }
}