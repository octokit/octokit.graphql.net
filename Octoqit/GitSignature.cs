namespace Octoqit
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    /// <summary>
    /// Information about a signature (GPG or S/MIME) on a Commit or Tag.
    /// </summary>
    public interface IGitSignature : IQueryEntity
    {
        /// <summary>
        /// Email used to sign this object.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// True if the signature is valid and verified by GitHub.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Payload for GPG signing object. Raw ODB object without the signature header.
        /// </summary>
        string Payload { get; }

        /// <summary>
        /// ASCII-armored signature header from object.
        /// </summary>
        string Signature { get; }

        /// <summary>
        /// GitHub user corresponding to the email signing this commit.
        /// </summary>
        User Signer { get; }

        /// <summary>
        /// The state of this signature. `VALID` if signature is valid and verified by GitHub, otherwise represents reason why signature is considered invalid.
        /// </summary>
        GitSignatureState State { get; }
    }
}

namespace Octoqit.Internal
{
    using System.Linq;
    using System.Linq.Expressions;
    using LinqToGraphQL;
    using LinqToGraphQL.Builders;

    internal class StubIGitSignature : QueryEntity, IGitSignature
    {
        public StubIGitSignature(IQueryProvider provider, Expression expression) : base(provider, expression)
        {
        }

        public string Email { get; }

        public bool IsValid { get; }

        public string Payload { get; }

        public string Signature { get; }

        public User Signer => this.CreateProperty(x => x.Signer, Octoqit.User.Create);

        public GitSignatureState State { get; }

        internal static StubIGitSignature Create(IQueryProvider provider, Expression expression)
        {
            return new StubIGitSignature(provider, expression);
        }
    }
}