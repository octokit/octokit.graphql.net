namespace Octokit.GraphQL.Model
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an unknown signature on a Commit or Tag.
    /// </summary>
    public class UnknownSignature : QueryEntity
    {
        public UnknownSignature(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        /// <summary>
        /// Email used to sign this object.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// True if the signature is valid and verified by GitHub.
        /// </summary>
        public bool IsValid { get; }

        /// <summary>
        /// Payload for GPG signing object. Raw ODB object without the signature header.
        /// </summary>
        public string Payload { get; }

        /// <summary>
        /// ASCII-armored signature header from object.
        /// </summary>
        public string Signature { get; }

        /// <summary>
        /// GitHub user corresponding to the email signing this commit.
        /// </summary>
        public User Signer => this.CreateProperty(x => x.Signer, Octokit.GraphQL.Model.User.Create);

        /// <summary>
        /// The state of this signature. `VALID` if signature is valid and verified by GitHub, otherwise represents reason why signature is considered invalid.
        /// </summary>
        public GitSignatureState State { get; }

        internal static UnknownSignature Create(IQueryProvider provider, Expression expression)
        {
            return new UnknownSignature(provider, expression);
        }
    }
}