namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A subset of repository info shared with potential collaborators.
    /// </summary>
    public class RepositoryInvitationRepository : QueryableValue<RepositoryInvitationRepository>
    {
        public RepositoryInvitationRepository(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset? CreatedAt { get; }

        /// <summary>
        /// The description of the repository.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The description of the repository rendered to HTML.
        /// </summary>
        public string DescriptionHTML { get; }

        /// <summary>
        /// Returns how many forks there are of this repository in the whole network.
        /// </summary>
        public int ForkCount { get; }

        /// <summary>
        /// Indicates if the repository has issues feature enabled.
        /// </summary>
        public bool HasIssuesEnabled { get; }

        /// <summary>
        /// Indicates if the repository has wiki feature enabled.
        /// </summary>
        public bool HasWikiEnabled { get; }

        /// <summary>
        /// The repository's URL.
        /// </summary>
        public string HomepageUrl { get; }

        /// <summary>
        /// Indicates if the repository is unmaintained.
        /// </summary>
        public bool IsArchived { get; }

        /// <summary>
        /// Identifies if the repository is a fork.
        /// </summary>
        public bool IsFork { get; }

        /// <summary>
        /// Indicates if the repository has been locked or not.
        /// </summary>
        public bool IsLocked { get; }

        /// <summary>
        /// Identifies if the repository is a mirror.
        /// </summary>
        public bool IsMirror { get; }

        /// <summary>
        /// Identifies if the repository is private.
        /// </summary>
        public bool IsPrivate { get; }

        /// <summary>
        /// The license associated with the repository
        /// </summary>
        [Obsolete(@"Use Repository.licenseInfo instead.")]
        public string License { get; }

        /// <summary>
        /// The license associated with the repository
        /// </summary>
        public License LicenseInfo => this.CreateProperty(x => x.LicenseInfo, Octokit.GraphQL.Model.License.Create);

        /// <summary>
        /// The reason the repository has been locked.
        /// </summary>
        public RepositoryLockReason? LockReason { get; }

        /// <summary>
        /// The repository's original mirror URL.
        /// </summary>
        public string MirrorUrl { get; }

        /// <summary>
        /// The name of the repository.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The repository's name with owner.
        /// </summary>
        public string NameWithOwner { get; }

        /// <summary>
        /// The owner of the repository associated with this invitation repository.
        /// </summary>
        public IRepositoryOwner Owner => this.CreateProperty(x => x.Owner, Octokit.GraphQL.Model.Internal.StubIRepositoryOwner.Create);

        /// <summary>
        /// Identifies when the repository was last pushed to.
        /// </summary>
        public DateTimeOffset? PushedAt { get; }

        /// <summary>
        /// The HTTP path for this repository
        /// </summary>
        public string ResourcePath { get; }

        /// <summary>
        /// A description of the repository, rendered to HTML without any links in it.
        /// </summary>
        /// <param name="limit">How many characters to return.</param>
        public string ShortDescriptionHTML(Arg<int>? limit = null) => null;

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        [Obsolete(@"General type updated timestamps will eventually be replaced by other field specific timestamps.")]
        public DateTimeOffset? UpdatedAt { get; }

        /// <summary>
        /// The HTTP URL for this repository
        /// </summary>
        public string Url { get; }

        internal static RepositoryInvitationRepository Create(Expression expression)
        {
            return new RepositoryInvitationRepository(expression);
        }
    }
}