namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
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

        /// <summary>
        /// The last time this authorization was used to perform an action
        /// </summary>
        public DateTimeOffset? AccessedAt { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The fingerprint for this PublicKey
        /// </summary>
        public string Fingerprint { get; }

        public ID Id { get; }

        /// <summary>
        /// Whether this PublicKey is read-only or not
        /// </summary>
        public bool IsReadOnly { get; }

        /// <summary>
        /// The public key string
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static PublicKey Create(Expression expression)
        {
            return new PublicKey(expression);
        }
    }
}