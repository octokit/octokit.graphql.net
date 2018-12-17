namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A user's public key.
    /// </summary>
    public class PublicKey : QueryableValue<PublicKey>
    {
        internal PublicKey(Expression expression) : base(expression)
        {
        }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public ID Id { get; }

        /// <summary>
        /// The public key string
        /// </summary>
        public string Key { get; }

        internal static PublicKey Create(Expression expression)
        {
            return new PublicKey(expression);
        }
    }
}