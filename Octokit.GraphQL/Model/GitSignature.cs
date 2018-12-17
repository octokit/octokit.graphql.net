namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Information about a signature (GPG or S/MIME) on a Commit or Tag.
    /// </summary>
    public interface IGitSignature : IQueryableValue<IGitSignature>, IQueryableInterface
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

        /// <summary>
        /// True if the signature was made with GitHub's signing key.
        /// </summary>
        bool WasSignedByGitHub { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    internal class StubIGitSignature : QueryableValue<StubIGitSignature>, IGitSignature
    {
        internal StubIGitSignature(Expression expression) : base(expression)
        {
        }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string Email { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public bool IsValid { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string Payload { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public string Signature { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public User Signer => this.CreateProperty(x => x.Signer, Octokit.GraphQL.Model.User.Create);

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public GitSignatureState State { get; }

        [SuppressMessage("System.Diagnostics", "CS1591", Justification = "Source did not provide detail")]
        public bool WasSignedByGitHub { get; }

        internal static StubIGitSignature Create(Expression expression)
        {
            return new StubIGitSignature(expression);
        }
    }
}