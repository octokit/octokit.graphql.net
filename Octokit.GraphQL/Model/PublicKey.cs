namespace Octokit.GraphQL.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A user's public key.
    /// </summary>
    public class PublicKey : QueryEntity
    {
        public PublicKey(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Id { get; }

        /// <summary>
        /// The public key string
        /// </summary>
        public string Key { get; }

        internal static PublicKey Create(IQueryProvider provider, Expression expression)
        {
            return new PublicKey(provider, expression);
        }
    }
}